using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        Task<IEnumerable<Guid>> GetUsersWithNotificationsAsync(
            Guid categoryId,
            IEnumerable<Guid> tagIds,
            Guid vendorId,
            Expression<Func<User, bool>> expression);

        Task<IEnumerable<Guid>> GetUsersWithNotificationsAsync(
            IEnumerable<Guid> categoryIds,
            IEnumerable<Guid> tagIds,
            Expression<Func<User, bool>> expression);

        IQueryable<Guid> GetUsersIds(
            Expression<Func<User, bool>> expression);

        Task UpdateStatusAsync(Guid id, bool isActive);

        Task UpdateRoleAsync(Guid id, UserRole role);

        Task UpdateNotificationsAsync(UserNotificationDto userNotifications);
    }
}