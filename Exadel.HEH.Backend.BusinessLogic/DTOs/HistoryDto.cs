using System;
using Exadel.HEH.Backend.DataAccess.Models;

namespace Exadel.HEH.Backend.BusinessLogic.DTOs
{
    public class HistoryDto
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public UserRole UserRole { get; set; }

        public string UserName { get; set; }

        public string UserEmail { get; set; }

        public AddressDto UserAddress { get; set; }

        public UserAction Action { get; set; }

        public string Description { get; set; }

        public DateTime DateTime { get; set; }
    }
}