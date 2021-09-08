using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Myriolang.ConlangDev.API.Commands.Languages;

namespace Myriolang.ConlangDev.API.Models
{
    public class Language
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string ProfileId { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string NativeName { get; set; }
        public string Description { get; set; }
        public List<string> Tags { get; set; }
        public DateTime Created { get; set; }

        public static Language NewFromMutation(CreateLanguageCommand mutation) => new()
        {
            ProfileId = mutation.ProfileId,
            Name = mutation.Name,
            NativeName = mutation.NativeName,
            Slug = mutation.Slug,
            Description = mutation.Description,
            Tags = mutation.Tags,
            Created = DateTime.Now
        };
    }
}