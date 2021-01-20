using System;
using MongoDB.Bson.Serialization.Attributes;

namespace Exadel.HEH.Backend.DataAccess.Models
{
    public class Phone
    {
        [BsonId]
        public Guid Id { get; set; }

        [BsonElement("number")]
        public string Number { get; set; }
    }
}