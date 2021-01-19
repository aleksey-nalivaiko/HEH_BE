using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Exadel.HEH.Backend.DataAccess.Models
{
    public class Link
    {
        [BsonElement("url")]
        public string Url { get; set; }

        [BsonRepresentation(BsonType.String)]
        public LinkTypeEnum Type { get; set; }

        public Guid Id { get; set; }
    }
}