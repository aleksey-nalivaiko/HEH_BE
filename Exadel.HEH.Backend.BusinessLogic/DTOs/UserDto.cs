using System;
using System.Collections.Generic;

namespace Exadel.HEH.Backend.BusinessLogic.DTOs
{
    public class UserDto
    {
        public Guid Id { get; set; }

        public UserRole Role { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public Address Address { get; set; }

        public bool IsActive { get; set; }

        public IList<Guid> CategoryNotifications { get; set; }

        public IList<Guid> TagNotifications { get; set; }

        public IList<Guid> VendorNotifications { get; set; }

        public bool NewVendorNotificationIsOn { get; set; }

        public bool NewDiscountNotificationIsOn { get; set; }

        public bool HotDiscountsNotificationIsOn { get; set; }

        public bool AllNotificationsAreOn { get; set; }

        public IEnumerable<FavoritesShortDto> Favorites { get; set; }
    }
}