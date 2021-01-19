using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Exadel.HEH.Backend.DataAccess.Models
{
    public class Link
    {
        public string Url { get; set; }

        public Enum Type { get; set; }

        [BsonRepresentation(BsonType.String)]
        [BsonElement("type")]
        public LinkType Type { get; set; }
    }
}