using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Myriolang.ConlangDev.API.Models
{
    public class Etymon
    {
        public string SourceLanguage { get; set; }
        public string Word { get; set; }
        public string Link { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string WordId { get; set; }
    }

    public class Etymology
    {
        public string Notes { get; set; }
        public List<Etymon> Etyma { get; set; }
    }

    public class Example
    {
        public string Text { get; set; }
        public string Gloss { get; set; }
        public string Source { get; set; }
    }
    
    public class Sense
    {
        public string PartOfSpeech { get; set; }
        public string Class { get; set; }
        public List<string> Glosses { get; set; }
        public List<Example> Examples { get; set; }
        public Etymology Etymology { get; set; }
        public string Notes { get; set; }
    }
    
    public class Word
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string LanguageId { get; set; }
        public string Headword { get; set; }
        public string Pronunciation { get; set; }
        public List<Sense> Senses { get; set; }
        public DateTime Created { get; set; }
    }
}