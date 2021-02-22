using System;

namespace Exadel.HEH.Backend.DataAccess.Models
{
    public class Notification : IDataModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Message { get; set; }

        public NotificationType Type { get; set; }

        public DateTime Date { get; set; }

        public bool IsRead { get; set; }

        public Guid SubjectId { get; set; }

        public Guid UserId { get; set; }
    }
}