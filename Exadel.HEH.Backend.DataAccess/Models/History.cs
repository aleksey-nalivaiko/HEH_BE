using System;

namespace Exadel.HEH.Backend.DataAccess.Models
{
    public class History : IDataModel
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public UserRole UserRole { get; set; }

        public string UserName { get; set; }

        public string UserEmail { get; set; }

        public Address UserAddress { get; set; }

        public UserAction Action { get; set; }

        public string Description { get; set; }

        public DateTime DateTime { get; set; }
    }
}