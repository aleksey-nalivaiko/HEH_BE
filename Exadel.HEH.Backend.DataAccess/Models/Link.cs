using System;
using MongoDB.Bson.Serialization.Attributes;

namespace Exadel.HEH.Backend.DataAccess.Models
{
    public class Link
    {
        [BsonElement("url")]
        public string Url { get; set; }

        public LinkType Type { get; set; }

        public Guid Id { get; set; }
    }
}