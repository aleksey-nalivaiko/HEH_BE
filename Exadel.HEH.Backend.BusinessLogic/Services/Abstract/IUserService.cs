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

        IQueryable<User> Get(Expression<Func<User, bool>> expression);

        Task<UserShortDto> GetByIdAsync(Guid id);

        Task<UserDto> GetProfileAsync();

        Task<Image> GetPhotoAsync(Guid id);

        Task<Image> GetPhotoAsync();

        Task<IEnumerable<User>> GetUsersWithNotificationsAsync(
            Guid categoryId,
            IEnumerable<Guid> tagIds,
            Guid vendorId,
            IList<Address> discountAddresses,
            Expression<Func<User, bool>> expression);

        Task<IEnumerable<User>> GetUsersWithNotificationsAsync(
            IEnumerable<Guid> categoryIds,
            IEnumerable<Guid> tagIds,
            IList<Address> vendorAddresses,
            Expression<Func<User, bool>> expression);

        Task UpdateStatusAsync(Guid id, bool isActive);

        Task UpdateRoleAsync(Guid id, UserRole role);

        Task UpdateNotificationsAsync(UserNotificationDto userNotifications);

        Task RemoveVendorSubscriptionsAsync(Guid vendorId);

        Task RemoveCategorySubscriptionsAsync(Guid categoryId);

        Task RemoveTagSubscriptionsAsync(Guid tagId);
    }
}