using System;
using Myriolang.ConlangDev.API.Models;

namespace Myriolang.ConlangDev.API.Mappers
{
    public class PublicProfile
    {
        public string Username { get; set; }
        public string Description { get; set; }
        public string DisplayName { get; set; }
        public DateTime Created { get; set; }

        public static PublicProfile FromProfile(Profile profile) => new PublicProfile
        {
            Username = profile.Username,
            Description = profile.Description,
            DisplayName = profile.DisplayName,
            Created = profile.Created
        };
    }
}