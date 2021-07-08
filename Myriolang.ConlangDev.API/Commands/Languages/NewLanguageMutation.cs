using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MediatR;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Myriolang.ConlangDev.API.Models;

namespace Myriolang.ConlangDev.API.Commands.Languages
{
    public class NewLanguageMutation : IRequest<Language>
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string ProfileId { get; set; }
        [Required]
        public string Name { get; set; }
        public string NativeName { get; set; }
        [Required]
        public string Slug { get; set; }
        public string Description { get; set; }
        public List<string> Tags { get; set; }
    }
}