using System;
using System.Collections.Generic;
using Exadel.HEH.Backend.DataAccess.Models;

namespace Exadel.HEH.Backend.BusinessLogic.DTOs.Get
{
    public class UserDto
    {
        public Guid Id { get; set; }

        public UserRole Role { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public AddressDto Address { get; set; }

        public bool IsActive { get; set; }

        public IList<Guid> CategoryNotifications { get; set; }

        public IList<Guid> TagNotifications { get; set; }

        public IList<Guid> VendorNotifications { get; set; }

        public bool NewVendorNotificationIsOn { get; set; }

        public bool NewDiscountNotificationIsOn { get; set; }

        public bool HotDiscountsNotificationIsOn { get; set; }

        public bool CityChangeNotificationIsOn { get; set; }

        public IList<FavoritesDto> Favorites { get; set; }
    }
}