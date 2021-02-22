using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs;
using Exadel.HEH.Backend.DataAccess.Models;

namespace Exadel.HEH.Backend.BusinessLogic.Services.Abstract
{
    public interface IUserService
    {
        IQueryable<UserShortDto> Get();

        Task<UserShortDto> GetByIdAsync(Guid id);

        Task<UserDto> GetProfileAsync();

        Task<IEnumerable<string>> GetEmailsForNotificationsAsync(
            IEnumerable<Guid> categoryIds,
            IEnumerable<Guid> tagIds,
            IEnumerable<Guid> vendorIds);

        Task UpdateStatusAsync(Guid id, bool isActive);

        Task UpdateRoleAsync(Guid id, UserRole role);

        Task UpdateNotificationsAsync(NotificationDto notifications);
    }
}