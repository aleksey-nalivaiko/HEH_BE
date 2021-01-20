using System;
using MongoDB.Bson.Serialization.Attributes;

namespace Exadel.HEH.Backend.DataAccess.Models
{
    public class PreOrder : IDataModel
    {
        [BsonId]
        public Guid Id { get; set; }

        [BsonElement("userId")]
        public Guid UserId { get; set; }

        [BsonElement("discountId")]
        public Guid DiscountId { get; set; }

        [BsonElement("orderDateTime")]
        public DateTime OrderTime { get; set; }

        [BsonElement("info")]
        public string Info { get; set; }
    }
}
