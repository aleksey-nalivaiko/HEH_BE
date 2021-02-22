using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;
using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Exadel.HEH.Backend.BusinessLogic.Services
{
    public class NotificationScheduler : INotificationScheduler
    {
        private readonly IEmailService _emailService;
        private readonly INotificationRepository _notificationRepository;

        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<NotificationScheduler> _logger;

        public NotificationScheduler(
            IEmailService emailService,
            IServiceScopeFactory serviceScopeFactory,
            INotificationRepository notificationRepository,
            ILogger<NotificationScheduler> logger)
        {
            _emailService = emailService;
            _notificationRepository = notificationRepository;
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        public void StartJobs()
        {
            RecurringJob.AddOrUpdate("HotNotifications", () => CreateHotNotificationsAsync(),
                "0 1 * * 1-5");

            //RecurringJob.AddOrUpdate("SendEmails", () => SendEmailsAsync(),
            //    Cron.Weekly(DayOfWeek.Friday, 11));
        }

        public async Task SendEmailsAsync()
        {
            //TODO: change email
            await _emailService.SendEmailAsync("<email>",
                "News from HEH", "Hi! Here you can found some discounts");

            _logger.LogInformation("Emails where send");
        }

        public async Task CreateHotNotificationsAsync()
        {
            using var scope = _serviceScopeFactory.CreateScope();

            var discountService = scope.ServiceProvider.GetService<IDiscountService>();
            var userService = scope.ServiceProvider.GetService<IUserService>();

            if (discountService != null && userService != null)
            {
                var discounts = discountService.GetHot();

                foreach (var discount in discounts)
                {
                    var userIds = (await userService.GetUsersWithNotificationsAsync(
                            discount.CategoryId,
                            discount.TagsIds,
                            discount.VendorId,
                            u => u.IsActive && u.AllNotificationsAreOn && u.HotDiscountsNotificationIsOn))
                        .ToList();

                    if (userIds.Any())
                    {
                        var notifications = new List<Notification>();

                        foreach (var userId in userIds)
                        {
                            var notification = new Notification
                            {
                                Title = $"Hot discount from {discount.VendorName}!",
                                Message = "We have a hot discount for you. Take your last chance to use it!",
                                Type = NotificationType.Hot,
                                SubjectId = discount.Id,
                                Date = DateTime.UtcNow,
                                IsRead = false,
                                UserId = userId
                            };

                            notifications.Add(notification);
                        }

                        await _notificationRepository.CreateManyAsync(notifications);
                    }
                }

                _logger.LogInformation("Hot notifications were sent to users");
            }
        }
    }
}