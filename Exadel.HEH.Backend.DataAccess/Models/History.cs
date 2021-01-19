using System;
using MongoDB.Bson.Serialization.Attributes;

namespace Exadel.HEH.Backend.DataAccess.Models
{
    public class History : IDataModel
    {
        [BsonId]
        public Guid Id { get; set; }

        [BsonElement("userId")]
        public Guid UserId { get; set; }

        [BsonElement("role")]
        public User.UserRole UserRole { get; set; }

        [BsonElement("name")]
        public string UserName { get; set; }

        [BsonElement("email")]
        public string UserEmail { get; set; }

        [BsonElement("action")]
        public string Action { get; set; }

        [BsonElement("dateTime")]
        public DateTime ActionDateTime { get; set; }
    }
}