using Exadel.HEH.Backend.DataAccess.Models;

namespace Exadel.HEH.Backend.Host.DTOs.Update
{
    public class UserUpdateDto
    {
        public UserRole Role { get; set; }

        public bool IsActive { get; set; }
    }
}