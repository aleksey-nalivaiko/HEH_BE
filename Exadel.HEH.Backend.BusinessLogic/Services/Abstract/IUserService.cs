using System;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs;
using Exadel.HEH.Backend.DataAccess.Models;

namespace Exadel.HEH.Backend.BusinessLogic.Services.Abstract
{
    public interface IUserService : IService<UserDto>
    {
        IQueryable<UserShortDto> Get();

        Task<UserShortDto> GetByIdAsync(Guid id);

        Task<UserDto> GetProfileAsync();

        Task UpdateStatusAsync(Guid id, bool isActive);

        Task UpdateRoleAsync(Guid id, UserRole role);

        Task UpdateNotificationsAsync(NotificationDto notifications);
    }
}