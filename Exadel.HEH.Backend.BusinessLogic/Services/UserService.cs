using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Update;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.BusinessLogic.Services
{
    public class UserService : BaseService<User, UserDto>, IUserService
    {
        private readonly IUserProvider _userProvider;
        private readonly IHistoryService _historyService;

        public UserService(IUserRepository repository, IMapper mapper, IUserProvider userProvider, IHistoryService historyService)
            : base(repository, mapper)
        {
            _userProvider = userProvider;
            _historyService = historyService;
        }

        public override async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var allUsers = await base.GetAllAsync();

            return allUsers.Where(u => u.Id != _userProvider.GetUserId());
        }

        public async Task<UserDto> GetByIdAsync(Guid id)
        {
            var result = await Repository.GetByIdAsync(id);
            return Mapper.Map<UserDto>(result);
        }

        public Task<UserDto> GetProfileAsync()
        {
            return GetByIdAsync(_userProvider.GetUserId());
        }

        public async Task UpdateNotificationsAsync(NotificationDto notifications)
        {
            var userId = _userProvider.GetUserId();
            var user = await Repository.GetByIdAsync(userId);

            user.NewVendorNotificationIsOn = notifications.NewVendorNotificationIsOn;
            user.NewDiscountNotificationIsOn = notifications.NewDiscountNotificationIsOn;
            user.HotDiscountsNotificationIsOn = notifications.HotDiscountsNotificationIsOn;
            user.AllNotificationsAreOn = notifications.AllNotificationsAreOn;

            user.TagNotifications = notifications.TagNotifications;
            user.CategoryNotifications = notifications.CategoryNotifications;
            user.VendorNotifications = notifications.VendorNotifications;

            await Repository.UpdateAsync(user);
            await _historyService.CreateAsync(UserAction.Edit,
                "Updated user " + userId);
        }

        public async Task UpdateStatusAsync(Guid id, bool isActive)
        {
            var user = await Repository.GetByIdAsync(id);
            user.IsActive = isActive;
            await Repository.UpdateAsync(user);
            await _historyService.CreateAsync(UserAction.Edit,
                "Updated user " + id);
        }

        public async Task UpdateRoleAsync(Guid id, UserRole role)
        {
            var user = await Repository.GetByIdAsync(id);
            user.Role = role;
            await Repository.UpdateAsync(user);
            await _historyService.CreateAsync(UserAction.Edit,
                "Updated user " + id);
        }
    }
}