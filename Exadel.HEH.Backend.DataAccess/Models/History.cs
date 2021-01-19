using System;

namespace Exadel.HEH.Backend.DataAccess.Models
{
    public class History : IDataModel
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public User.UserRole UserRole { get; set; }

        public string UserName { get; set; }

        public string UserEmail { get; set; }

        public string Action { get; set; }

        public DateTime ActionDateTime { get; set; }
    }
}