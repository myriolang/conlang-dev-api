using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson.IO;
using MongoDB.Driver;
using Myriolang.ConlangDev.API.Commands.Profiles;
using Myriolang.ConlangDev.API.Models;
using Myriolang.ConlangDev.API.Models.Responses;
using Newtonsoft.Json;

namespace Myriolang.ConlangDev.API.Services.Default
{
    public class ProfileService : IProfileService
    {
        private readonly IMongoCollection<Profile> _profiles;

        public ProfileService(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetSection("MongoDB")["ConnectionString"]);
            var database = client.GetDatabase(configuration.GetSection("MongoDB")["DatabaseName"]);
            _profiles = database.GetCollection<Profile>("Profiles");
        }

        public async Task<Profile> FindById(string id, CancellationToken cancellationToken)
            => await _profiles
                .Find(p => p.Id == id)
                .FirstOrDefaultAsync(cancellationToken)
                .ConfigureAwait(false);

        public async Task<Profile> FindByUsername(string username, CancellationToken cancellationToken)
            => await _profiles
                .Find(p => p.Username == username)
                .FirstOrDefaultAsync(cancellationToken)
                .ConfigureAwait(false);

        public async Task<Profile> Create(CreateProfileCommand request, CancellationToken cancellationToken)
        {
            var profile = new Profile
            {
                Username = request.Username,
                Email = request.Email,
                Hash = HashPassword(request.Password),
                Created = DateTime.Now
            };
            try
            {
                await _profiles
                    .InsertOneAsync(profile, cancellationToken: cancellationToken)
                    .ConfigureAwait(false);
                return profile;
            }
            catch
            {
                return null;
            }
        }

        public bool VerifyProfilePassword(Profile profile, string candidate) 
            => VerifyPassword(candidate, profile.Hash);

        public async Task<ValidationResponse> ValidateUsername(string username, CancellationToken cancellationToken)
        {
            var count = await _profiles
                .CountDocumentsAsync(p => p.Username == username, cancellationToken: cancellationToken)
                .ConfigureAwait(false);
            var response = new ValidationResponse
            {
                Field = "username",
                Value = username
            };
            response.Valid = count == 0;
            response.Message = count > 0 ? "Username already exists" : null;
            return response;
        }

        public async Task<ValidationResponse> ValidateEmail(string email, CancellationToken cancellationToken)
        {
            var count = await _profiles
                .CountDocumentsAsync(p => p.Email == email, cancellationToken: cancellationToken)
                .ConfigureAwait(false);
            var response = new ValidationResponse
            {
                Field = "email",
                Value = email
            };
            response.Valid = count == 0;
            response.Message = count > 0 ? "Email already in use" : null;
            return response;
        }

        private string HashPassword(string password)
            => BCrypt.Net.BCrypt.EnhancedHashPassword(password);

        private bool VerifyPassword(string candidate, string password)
            => BCrypt.Net.BCrypt.EnhancedVerify(candidate, password);
    }
}