using System;
using System.Diagnostics.CodeAnalysis;

namespace Exadel.HEH.Backend.DataAccess.Models
{
    [ExcludeFromCodeCoverage]
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