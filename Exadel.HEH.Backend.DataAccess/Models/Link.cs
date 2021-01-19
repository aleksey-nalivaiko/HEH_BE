using System;
using MongoDB.Bson.Serialization.Attributes;

namespace Exadel.HEH.Backend.DataAccess.Models
{
    public class Link
    {
        public enum LinkType
        {
            Facebook,
            Instagram,
            Website
        }

        [BsonElement("url")]
        public string Url { get; set; }

        [BsonElement("type")]
        public LinkType Type { get; set; }
    }
}