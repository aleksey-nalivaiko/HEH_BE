using System;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Update;
using Exadel.HEH.Backend.DataAccess.Models;

namespace Exadel.HEH.Backend.BusinessLogic.Services.Abstract
{
    public interface IUserService
    {
        IQueryable<UserShortDto> Get();

        Task<UserDto> GetByIdAsync(Guid id);

        Task<UserDto> GetProfileAsync();

        Task UpdateStatusAsync(Guid id, bool isActive);

        Task UpdateRoleAsync(Guid id, UserRole role);

        Task UpdateNotificationsAsync(NotificationDto notifications);
    }
}