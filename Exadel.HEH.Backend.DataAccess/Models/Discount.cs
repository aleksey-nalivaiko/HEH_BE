using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace Exadel.HEH.Backend.DataAccess.Models
{
    public class Discount
    {
        [BsonId]
        public Guid Id { get; set; }

        [BsonElement("conditions")]
        public string Conditions { get; set; }

        [BsonElement("tagsIds")]
        public List<Tag> Tags { get; set; }

        [BsonElement("vendorId")]
        public Guid VendorId { get; set; }

        [BsonElement("promoCode")]
        public string PromoCode { get; set; }

        [BsonElement("addresses")]
        public List<Address> Addresses { get; set; }

        [BsonElement("startDate")]
        public DateTime StartDate { get; set; }

        [BsonElement("endDate")]
        public DateTime EndDate { get; set; }

        [BsonElement("categoryId")]
        public Guid CategoryId { get; set; }
    }
}