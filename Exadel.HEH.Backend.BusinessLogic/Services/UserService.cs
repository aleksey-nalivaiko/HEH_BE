using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Exadel.HEH.Backend.BusinessLogic.DTOs;
using Exadel.HEH.Backend.BusinessLogic.Providers;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserProvider _userProvider;
        private readonly IHistoryService _historyService;
        private readonly IMapper _mapper;

        public UserService(
            IUserRepository userRepository,
            IMapper mapper,
            IUserProvider userProvider,
            IHistoryService historyService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userProvider = userProvider;
            _historyService = historyService;
        }

        public IQueryable<UserShortDto> Get()
        {
            var currentUserId = _userProvider.GetUserId();
            var users = _userRepository.Get().Where(u => u.Id != currentUserId);

            return users.ProjectTo<UserShortDto>(_mapper.ConfigurationProvider);
        }

        public IQueryable<User> Get(Expression<Func<User, bool>> expression)
        {
            return _userRepository.Get().Where(expression);
        }

        public async Task<UserShortDto> GetByIdAsync(Guid id)
        {
            var result = await _userRepository.GetByIdAsync(id);
            return _mapper.Map<UserShortDto>(result);
        }

        public async Task<UserDto> GetProfileAsync()
        {
            var result = await _userRepository.GetByIdAsync(_userProvider.GetUserId());
            return _mapper.Map<UserDto>(result);
        }

        public async Task<IEnumerable<User>> GetUsersWithNotificationsAsync(
            Guid categoryId,
            IEnumerable<Guid> tagIds,
            Guid vendorId,
            Expression<Func<User, bool>> expression)
        {
            var users = new List<User>();

            await Task.WhenAll(
                GetWithSubscriptionsAsync(u => u.CategoryNotifications, expression, categoryId, users),
                GetWithSubscriptionsAsync(u => u.TagNotifications, expression, tagIds, users),
                GetWithSubscriptionsAsync(u => u.VendorNotifications, expression, vendorId, users));

            return users.Distinct();
        }

        public async Task<IEnumerable<User>> GetUsersWithNotificationsAsync(
            IEnumerable<Guid> categoryIds,
            IEnumerable<Guid> tagIds,
            Expression<Func<User, bool>> expression)
        {
            var users = new List<User>();

            await Task.WhenAll(
                GetWithSubscriptionsAsync(u => u.CategoryNotifications, expression, categoryIds, users),
                GetWithSubscriptionsAsync(u => u.TagNotifications, expression, tagIds, users));

            return users.Distinct();
        }

        public async Task UpdateNotificationsAsync(UserNotificationDto userNotifications)
        {
            var userId = _userProvider.GetUserId();
            var user = await _userRepository.GetByIdAsync(userId);

            user.NewVendorNotificationIsOn = userNotifications.NewVendorNotificationIsOn;
            user.NewDiscountNotificationIsOn = userNotifications.NewDiscountNotificationIsOn;
            user.HotDiscountsNotificationIsOn = userNotifications.HotDiscountsNotificationIsOn;
            user.AllNotificationsAreOn = userNotifications.AllNotificationsAreOn;

            user.TagNotifications = userNotifications.TagNotifications;
            user.CategoryNotifications = userNotifications.CategoryNotifications;
            user.VendorNotifications = userNotifications.VendorNotifications;

            await _userRepository.UpdateAsync(user);
        }

        public async Task UpdateStatusAsync(Guid id, bool isActive)
        {
            var user = await _userRepository.GetByIdAsync(id);
            user.IsActive = isActive;
            await _userRepository.UpdateAsync(user);
            await _historyService.CreateAsync(UserAction.Edit,
                "Updated user " + user.Name + " status");
        }

        public async Task UpdateRoleAsync(Guid id, UserRole role)
        {
            var user = await _userRepository.GetByIdAsync(id);
            user.Role = role;
            await _userRepository.UpdateAsync(user);
            await _historyService.CreateAsync(UserAction.Edit,
                "Updated user " + user.Name + " role");
        }

        private async Task GetWithSubscriptionsAsync(
            Expression<Func<User, IEnumerable<Guid>>> inField,
            Expression<Func<User, bool>> expression,
            Guid value,
            List<User> users)
        {
            users.AddRange(
                await _userRepository.GetWithSubscriptionAsync(
                    inField, expression, value));
        }

        private async Task GetWithSubscriptionsAsync(
            Expression<Func<User, IEnumerable<Guid>>> inField,
            Expression<Func<User, bool>> expression,
            IEnumerable<Guid> inValues,
            List<User> users)
        {
            users.AddRange(
                await _userRepository.GetWithSubscriptionsAsync(
                    inField, expression, inValues));
        }
    }
}