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
            RecurringJob.AddOrUpdate("HotNotifications", () => SendHotNotificationsAsync(),
                "0 1 * * 1-5");

            //TODO: weekly count of not read
            //RecurringJob.AddOrUpdate("SendEmails", () => SendEmailsAsync(),
            //    Cron.Weekly(DayOfWeek.Friday, 11));
        }

        public async Task SendHotNotificationsAsync()
        {
            var notifications = await GetHotNotificationsAsync();

            if (notifications.Any())
            {
                await _notificationRepository.CreateManyAsync(
                    notifications.Select(t => t.notification));

                _logger.LogInformation("Hot notifications were created.");

                await SendEmailsAsync(notifications);

                _logger.LogInformation("Emails with hot notifications where send.");
            }
        }

        public Task SendEmailsAsync(List<(Notification notification, User user)> notifications)
        {
            var emailTasks = new List<Task>();

            foreach (var notification in notifications)
            {
                emailTasks.Add(_emailService.SendEmailAsync(notification.user.Email,
                    "Hot discounts in HEH", "Hi! Here you can found some discounts"));
            }

            return Task.WhenAll(emailTasks);
        }

        private async Task<List<(Notification notification, User user)>> GetHotNotificationsAsync()
        {
            var notifications = new List<(Notification notification, User user)>();

            using var scope = _serviceScopeFactory.CreateScope();

            var discountService = scope.ServiceProvider.GetService<IDiscountService>();
            var userService = scope.ServiceProvider.GetService<IUserService>();

            if (discountService != null && userService != null)
            {
                var discounts = discountService.GetHot();

                foreach (var discount in discounts)
                {
                    var users = (await userService.GetUsersWithNotificationsAsync(
                            discount.CategoryId,
                            discount.TagsIds,
                            discount.VendorId,
                            u => u.IsActive && u.AllNotificationsAreOn && u.HotDiscountsNotificationIsOn))
                        .ToList();

                    foreach (var user in users)
                    {
                        var notification = new Notification
                        {
                            Title = $"Hot discount from {discount.VendorName}!",
                            Message = "We have a hot discount for you. Take your last chance to use it!",
                            Type = NotificationType.Hot,
                            SubjectId = discount.Id,
                            Date = DateTime.UtcNow,
                            IsRead = false,
                            UserId = user.Id
                        };

                        notifications.Add((notification, user));
                    }
                }
            }

            return notifications;
        }
    }
}