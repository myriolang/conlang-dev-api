using Autofac;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Myriolang.ConlangDev.API.Middleware;
using Myriolang.ConlangDev.API.Services;
using Myriolang.ConlangDev.API.Services.Default;
using Myriolang.ConlangDev.API.Services.Setup;

namespace Myriolang.ConlangDev.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddAuthentication("ConlangDevAuthentication")
                .AddScheme<AuthenticationSchemeOptions, ConlangDevAuthenticationHandler>("ConlangDevAuthentication",
                    null);
            
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Myriolang.ConlangDev.API", Version = "v1"});
            });
            services.AddHostedService<DatabaseSetupService>();
        }

        // Configure dependency injection/IoC container
        public void ConfigureContainer(ContainerBuilder builder)
        {
            // Services
            builder.RegisterType<AuthService>().As<IAuthService>().InstancePerLifetimeScope();
            builder.RegisterType<ProfileService>().As<IProfileService>().InstancePerLifetimeScope();
            builder.RegisterType<LanguageService>().As<ILanguageService>().InstancePerLifetimeScope();
            builder.RegisterType<WordService>().As<IWordService>().InstancePerLifetimeScope();
            // Mediator & its handlers
            builder.RegisterType<Mediator>().As<IMediator>().InstancePerLifetimeScope();
            builder.Register<ServiceFactory>(context =>
            {
                var c = context.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });
            builder
                .RegisterAssemblyTypes(typeof(Startup).Assembly)
                .Where(t => t.IsClosedTypeOf(typeof(IRequestHandler<,>)))
                .AsImplementedInterfaces();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Myriolang.ConlangDev.API v1"));
            }

            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true)
                .AllowCredentials());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}