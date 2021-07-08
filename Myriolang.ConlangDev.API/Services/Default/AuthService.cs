using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Myriolang.ConlangDev.API.Models;
using Myriolang.ConlangDev.API.Models.Responses;

namespace Myriolang.ConlangDev.API.Services.Default
{
    public class AuthService : IAuthService
    {
        private readonly IProfileService _profileService;
        private readonly SymmetricSecurityKey _key;

        public AuthService(IProfileService profileService, IConfiguration configuration)
        {
            _profileService = profileService;
            _key = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(configuration.GetSection("Secrets")["JwtSecret"])
            );
        }
        
        public async Task<AuthenticationResponse> Authenticate(string username, string password)
        {
            var profile = await _profileService.FindByUsername(username);
            if (profile is null) return null;
            if (!_profileService.VerifyProfilePassword(profile, password)) return null;
            return GenerateResponse(profile);
        }

        public async Task<Profile> ValidateToken(string jwt)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                handler.ValidateToken(jwt, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = _key,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);
                var id = ((JwtSecurityToken) validatedToken).Claims.First(c => c.Type == "id").Value;
                return await _profileService.FindById(id);
            }
            catch
            {
                return null;
            }
        }

        private AuthenticationResponse GenerateResponse(Profile profile)
        {
            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("id", profile.Id)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256Signature)
            };
            var handler = new JwtSecurityTokenHandler();
            var token = handler.CreateToken(descriptor);
            return new AuthenticationResponse
            {
                Token = handler.WriteToken(token),
                Profile = profile
            };
        }
    }
}