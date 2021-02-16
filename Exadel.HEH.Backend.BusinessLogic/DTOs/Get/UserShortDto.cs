using System;
using Exadel.HEH.Backend.DataAccess.Models;

namespace Exadel.HEH.Backend.BusinessLogic.DTOs.Get
{
    public class UserShortDto
    {
        public Guid Id { get; set; }

        public UserRole Role { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public AddressDto Address { get; set; }

        public bool IsActive { get; set; }

        public string Img { get; set; }
    }
}