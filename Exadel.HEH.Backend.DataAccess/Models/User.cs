using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace Exadel.HEH.Backend.DataAccess.Models
{
    public class User : IDataModel
    {
        public enum UserRole
        {
            Employee,
            Moderator,
            Administrator
        }

        [BsonId]
        public Guid Id { get; set; }

        [BsonElement("role")]
        public UserRole Role { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("password")]
        public string Password { get; set; }

        [BsonElement("address")]
        public Address Office { get; set; }

        [BsonElement("isActive")]
        public bool IsActive { get; set; }

        [BsonElement("categoryNotifications")]
        public IList<Guid> CategoryNotificationsId { get; set; }

        [BsonElement("tagNotifications")]
        public IList<Guid> TagNotificationsId { get; set; }

        [BsonElement("vendorNotifications")]
        public IList<Guid> VendorNotificationsId { get; set; }

        [BsonElement("newVendorNotificationIsOn")]
        public bool NewVendorNotificationIsOn { get; set; }

        [BsonElement("newDiscountNotificationIsOn")]
        public bool NewDiscountNotificationIsOn { get; set; }

        [BsonElement("hotDiscountsNotificationIsOn")]
        public bool HotDiscountsNotificationIsOn { get; set; }

        [BsonElement("cityChangeNotificationIsOn")]
        public bool CityChangeNotificationIsOn { get; set; }

        [BsonElement("favorites")]
        public IList<Favorites> Favorites { get; set; }
    }
}
