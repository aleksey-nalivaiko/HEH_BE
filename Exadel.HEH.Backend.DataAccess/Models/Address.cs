using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;

namespace Exadel.HEH.Backend.DataAccess.Models
{
    public class Address
    {
        [BsonId]
        public Guid Id { get; set; }

        [BsonElement("country")]
        public string Country { get; set; }

        [BsonElement("city")]
        public string City { get; set; }

        [BsonElement("street")]
        public string Street { get; set; }
    }
}