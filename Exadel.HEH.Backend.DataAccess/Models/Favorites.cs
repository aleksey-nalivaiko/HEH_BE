using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;

namespace Exadel.HEH.Backend.DataAccess.Models
{
    public class Favorites
    {
        [BsonElement("discountId")]
        public Guid DiscountId { get; set; }

        [BsonElement("note")]
        public string Note { get; set; }
    }
}
