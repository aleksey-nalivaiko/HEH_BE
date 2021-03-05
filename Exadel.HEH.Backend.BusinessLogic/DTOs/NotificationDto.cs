using System;
using Exadel.HEH.Backend.DataAccess.Models;

namespace Exadel.HEH.Backend.BusinessLogic.DTOs
{
    public class NotificationDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Message { get; set; }

        public NotificationType Type { get; set; }

        public DateTime Date { get; set; }

        public bool IsRead { get; set; }
    }
}