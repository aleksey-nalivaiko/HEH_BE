using System;
using MongoDB.Bson.Serialization.Attributes;

namespace Exadel.HEH.Backend.DataAccess.Models
{
    public class Link
    {
        [BsonElement("url")]
        public string Url { get; set; }

        [BsonElement("type")]
        public Enum Type { get; set; }
    }
}