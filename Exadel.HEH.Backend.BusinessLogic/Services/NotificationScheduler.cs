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

            RecurringJob.AddOrUpdate("NotificationsCount", () =>
                    SendNotificationsCountAsync(),
                Cron.Weekly(DayOfWeek.Friday, 18));
        }

        public async Task SendHotNotificationsAsync()
        {
            var notifications = await GetHotNotificationsAsync();

            if (notifications.Any())
            {
                await _notificationRepository.CreateManyAsync(
                    notifications.Select(t => t.notification));

                _logger.LogInformation("Hot notifications were created.");

                var allUserNotifications = notifications
                    .GroupBy(n => (n.user!.Email, n.user.Name), t => t)
                    .Select(g =>
                        new KeyValuePair<(string, string), IEnumerable<Notification>>(
                            g.Key, g.Select(n => n.notification)))
                    .ToDictionary(p => p.Key, p => p.Value);

                await SendHotNotificationEmailsAsync(allUserNotifications);

                _logger.LogInformation("Emails with hot notifications were sent.");
            }
        }

        public async Task SendNotificationsCountAsync()
        {
            var notifications = await GetNotificationsCountAsync();

            if (notifications.Any())
            {
                var userNotificationsCount = notifications
                    .Select(n =>
                        new KeyValuePair<(string, string), int>(
                            (n.user!.Email, n.user.Name), n.count))
                    .ToDictionary(p => p.Key, p => p.Value);

                await SendNotificationsCountEmailAsync(userNotificationsCount);

                _logger.LogInformation("Emails with notifications count were sent.");
            }
        }

        private Task SendHotNotificationEmailsAsync(IDictionary<(string, string), IEnumerable<Notification>> notifications)
        {
            var emailTasks = new List<Task>();

            foreach (var ((userEmail, userName), userNotifications) in notifications)
            {
                var userNotificationsList = userNotifications.ToList();

                if (userNotificationsList.Any())
                {
                    emailTasks.Add(
                        _emailService.SendEmailAsync(
                            userEmail,
                            "Hot discounts from HEH",
                            _emailService.CompleteHotNotificationsMessage(
                                userNotificationsList, userName)));
                }
            }

            return Task.WhenAll(emailTasks);
        }

        private Task SendNotificationsCountEmailAsync(IDictionary<(string, string), int> notificationsCount)
        {
            var emailTasks = new List<Task>();

            foreach (var ((userEmail, userName), count) in notificationsCount)
            {
                if (count != 0)
                {
                    emailTasks.Add(
                        _emailService.SendEmailAsync(
                            userEmail,
                            "Notifications from HEH",
                            _emailService.CompleteNotificationsCountMessage(
                                count, userName)));
                }
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
                            discount.Addresses,
                            u => u.IsActive && u.AllNotificationsAreOn && u.HotDiscountsNotificationIsOn))
                        .ToList();

                    foreach (var user in users)
                    {
                        var notification = new Notification
                        {
                            Title = $"Hot discount from {discount.VendorName}!",
                            Message = $"We have a hot discount for you: {discount.Conditions}. " +
                                      $"Last day you can use it: {discount.EndDate:dd.MM.yyyy}",
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

        private async Task<List<(int count, User user)>> GetNotificationsCountAsync()
        {
            var notificationsCount = new List<(int count, User user)>();

            using var scope = _serviceScopeFactory.CreateScope();

            var userService = scope.ServiceProvider.GetService<IUserService>();

            if (userService != null)
            {
                var users = userService.Get(u => u.IsActive && u.AllNotificationsAreOn).ToList();

                foreach (var user in users)
                {
                    var count = (await _notificationRepository.GetAsync(
                        n => n.UserId == user.Id && !n.IsRead)).Count();

                    notificationsCount.Add((count, user));
                }
            }

            return notificationsCount;
        }
    }
}