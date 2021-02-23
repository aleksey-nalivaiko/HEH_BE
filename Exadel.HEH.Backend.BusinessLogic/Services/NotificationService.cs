using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Exadel.HEH.Backend.BusinessLogic.DTOs;
using Exadel.HEH.Backend.BusinessLogic.Providers;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;
using Microsoft.Extensions.Logging;

namespace Exadel.HEH.Backend.BusinessLogic.Services
{
    public class NotificationService : INotificationService
    {
        private readonly ILogger<NotificationService> _logger;
        private readonly IUserService _userService;
        private readonly IVendorSearchService _vendorService;
        private readonly IUserProvider _userProvider;
        private readonly INotificationRepository _notificationRepository;
        private readonly IMapper _mapper;

        public NotificationService(
            INotificationRepository notificationRepository,
            ILogger<NotificationService> logger,
            IUserService userService,
            IVendorSearchService vendorService,
            IUserProvider userProvider,
            IMapper mapper)
        {
            _notificationRepository = notificationRepository;
            _logger = logger;
            _userService = userService;
            _vendorService = vendorService;
            _userProvider = userProvider;
            _mapper = mapper;
        }

        public IQueryable<NotificationDto> Get()
        {
            var userId = _userProvider.GetUserId();

            var notifications = _notificationRepository.Get()
                .Where(n => n.UserId == userId).OrderByDescending(n => n.Date);

            return _mapper.ProjectTo<NotificationDto>(notifications);
        }

        public async Task<NotificationDto> GetByIdAsync(Guid id)
        {
            var notification = await _notificationRepository.GetByIdAsync(id);
            notification.IsRead = true;

            await _notificationRepository.UpdateAsync(notification);

            return _mapper.Map<NotificationDto>(notification);
        }

        public async Task<int> GetNotReadCountAsync()
        {
            var userId = _userProvider.GetUserId();

            return (await _notificationRepository.GetAsync(
                    n => n.UserId == userId && !n.IsRead))
                .Count();
        }

        public async Task CreateVendorNotificationsAsync(Guid vendorId)
        {
            var vendor = await _vendorService.GetByIdAsync(vendorId);

            Expression<Func<User, bool>> expression = u =>
                u.IsActive && u.AllNotificationsAreOn && u.NewVendorNotificationIsOn;

            var users = (!vendor.CategoriesIds.Any() && !vendor.TagsIds.Any()
                ? _userService.Get(expression)
                : await _userService.GetUsersWithNotificationsAsync(
                    vendor.CategoriesIds,
                    vendor.TagsIds,
                    expression))
                .ToList();

            if (users.Any())
            {
                var notifications = new List<Notification>();

                foreach (var user in users)
                {
                    var notification = new Notification
                    {
                        Title = $"New vendor: {vendor.Vendor}!",
                        Message = "New vendor added. You might be interested: check detailed info.",
                        Type = NotificationType.Vendor,
                        SubjectId = vendor.Id,
                        Date = DateTime.UtcNow,
                        IsRead = false,
                        UserId = user.Id
                    };
                    notifications.Add(notification);
                }

                await _notificationRepository.CreateManyAsync(notifications);

                _logger.LogInformation("Vendor notifications were sent to users");
            }
        }

        public async Task CreateDiscountNotificationsAsync(Discount discount)
        {
            var users = (await _userService.GetUsersWithNotificationsAsync(
                    discount.CategoryId,
                    discount.TagsIds,
                    discount.VendorId,
                    u => u.IsActive && u.AllNotificationsAreOn && u.NewDiscountNotificationIsOn))
                .ToList();

            if (users.Any())
            {
                var notifications = new List<Notification>();

                foreach (var user in users)
                {
                    var notification = new Notification
                    {
                        Title = $"New discount from {discount.VendorName}!",
                        Message = "New discount added. You might be interested: check detailed info.",
                        Type = NotificationType.Discount,
                        SubjectId = discount.Id,
                        Date = DateTime.UtcNow,
                        IsRead = false,
                        UserId = user.Id
                    };

                    notifications.Add(notification);
                }

                await _notificationRepository.CreateManyAsync(notifications);

                _logger.LogInformation("Discount notifications were sent to users");
            }
        }

        public Task RemoveDiscountNotificationsAsync(Guid discountId)
        {
            return _notificationRepository.RemoveAsync(n =>
                (n.Type == NotificationType.Discount || n.Type == NotificationType.Hot)
                && n.SubjectId == discountId);
        }

        public Task RemoveVendorNotificationsAsync(Guid vendorId)
        {
            return _notificationRepository.RemoveAsync(n =>
                n.Type == NotificationType.Vendor && n.SubjectId == vendorId);
        }
    }
}