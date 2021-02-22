using System;
using System.Collections.Generic;

namespace Exadel.HEH.Backend.BusinessLogic.DTOs
{
    public class UserNotificationDto
    {
        public IList<Guid> CategoryNotifications { get; set; }

        public IList<Guid> TagNotifications { get; set; }

        public IList<Guid> VendorNotifications { get; set; }

        public bool NewVendorNotificationIsOn { get; set; }

        public bool NewDiscountNotificationIsOn { get; set; }

        public bool HotDiscountsNotificationIsOn { get; set; }

        public bool AllNotificationsAreOn { get; set; }
    }
}