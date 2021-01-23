using System;
using Exadel.HEH.Backend.DataAccess.Models;

namespace Exadel.HEH.Backend.BusinessLogic.DTOs.Update
{
    public class UserUpdateDto
    {
        public Guid Id { get; set; }

        public UserRole Role { get; set; }

        public bool IsActive { get; set; }
    }
}