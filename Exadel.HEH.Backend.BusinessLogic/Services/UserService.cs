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

            var users = _userRepository.Get().ToList()
                .Where(u => u.Id != currentUserId).AsQueryable();

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
            IList<Address> discountAddresses,
            Expression<Func<User, bool>> expression)
        {
            var users = new List<User>();

            await Task.WhenAll(
                GetWithSubscriptionsAsync(u => u.CategoryNotifications, expression, categoryId, users),
                GetWithSubscriptionsAsync(u => u.TagNotifications, expression, tagIds, users),
                GetWithSubscriptionsAsync(u => u.VendorNotifications, expression, vendorId, users));

            return FilterUsers(discountAddresses, users);
        }

        public async Task<IEnumerable<User>> GetUsersWithNotificationsAsync(
            IEnumerable<Guid> categoryIds,
            IEnumerable<Guid> tagIds,
            IList<Address> vendorAddresses,
            Expression<Func<User, bool>> expression)
        {
            var users = new List<User>();

            await Task.WhenAll(
                GetWithSubscriptionsAsync(u => u.CategoryNotifications, expression, categoryIds, users),
                GetWithSubscriptionsAsync(u => u.TagNotifications, expression, tagIds, users));

            return FilterUsers(vendorAddresses, users);
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

        public async Task RemoveVendorSubscriptionsAsync(Guid vendorId)
        {
            var users = (await _userRepository.GetWithSubscriptionAsync(
                u => u.VendorNotifications, vendorId)).ToList();

            if (users.Any())
            {
                foreach (var user in users)
                {
                    var vendorNotifications = user.VendorNotifications.ToList();
                    vendorNotifications.RemoveAll(v => v == vendorId);
                    user.VendorNotifications = vendorNotifications;
                }

                await _userRepository.UpdateManyAsync(users);
            }
        }

        public async Task RemoveCategorySubscriptionsAsync(Guid categoryId)
        {
            var users = (await _userRepository.GetWithSubscriptionAsync(
                u => u.CategoryNotifications, categoryId)).ToList();

            if (users.Any())
            {
                foreach (var user in users)
                {
                    var categoryNotifications = user.CategoryNotifications.ToList();
                    categoryNotifications.RemoveAll(c => c == categoryId);
                    user.CategoryNotifications = categoryNotifications;
                }

                await _userRepository.UpdateManyAsync(users);
            }
        }

        public async Task RemoveTagSubscriptionsAsync(Guid tagId)
        {
            var users = (await _userRepository.GetWithSubscriptionAsync(
                u => u.TagNotifications, tagId)).ToList();

            if (users.Any())
            {
                foreach (var user in users)
                {
                    var tagNotifications = user.TagNotifications.ToList();
                    tagNotifications.RemoveAll(t => t == tagId);
                    user.TagNotifications = tagNotifications;
                }

                await _userRepository.UpdateManyAsync(users);
            }
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
                    inField, value, expression));
        }

        private async Task GetWithSubscriptionsAsync(
            Expression<Func<User, IEnumerable<Guid>>> inField,
            Expression<Func<User, bool>> expression,
            IEnumerable<Guid> inValues,
            List<User> users)
        {
            users.AddRange(
                await _userRepository.GetWithSubscriptionsAsync(
                    inField, inValues, expression));
        }

        private IEnumerable<User> FilterUsers(IList<Address> addresses, IEnumerable<User> users)
        {
            var countryCities = addresses
                .GroupBy(a => a.CountryId)
                .Select(g =>
                    new KeyValuePair<Guid, IEnumerable<Guid?>>(
                        g.Key, g.Select(a => a.CityId).Where(i => i.HasValue)))
                .ToDictionary(a => a.Key, a => a.Value);

            return users
                .GroupBy(u => u.Id)
                .Select(g => g.First())
                .Where(u => countryCities.ContainsKey(u.Address.CountryId)
                            && (!countryCities[u.Address.CountryId].Any()
                                || countryCities[u.Address.CountryId].Contains(u.Address.CityId)));
        }
    }
}