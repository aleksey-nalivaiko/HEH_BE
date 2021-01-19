using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
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

        public Guid Id { get; set; }

        [BsonElement("role")]
        public UserRole Role { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public Address Office { get; set; }

        public bool IsActive { get; set; }

        public Guid[] CategoryNotificationsId { get; set; }

        public Guid[] TagNotificationsId { get; set; }

        public Guid[] VendorNotificationsId { get; set; }

        public bool NewVendorNotificationIsOn { get; set; }

        public bool NewDiscountNotificationIsOn { get; set; }

        public bool HotDiscountsNotificationIsOn { get; set; }

        public bool CityChangeNotificationIsOn { get; set; }

        public Favorites[] Favorites { get; set; }
    }
}
