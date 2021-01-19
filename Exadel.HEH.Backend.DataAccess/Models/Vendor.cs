using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace Exadel.HEH.Backend.DataAccess.Models
{
    public class Vendor
    {
        [BsonId]
        public Guid Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("links")]
        public IList<Link> Links { get; set; }

        [BsonElement("mailing")]
        public bool Mailing { get; set; }

        [BsonElement("phones")]
        public IList<Phone> Phone { get; set; }

        [BsonElement("addresses")]
        public IList<Address> Addresses { get; set; }

        [BsonElement("viewsAmount")]
        public int ViewsAmount { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }
    }
}