using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Hangfire;
using Microsoft.Extensions.Logging;

namespace Exadel.HEH.Backend.BusinessLogic.Services
{
    public class NotificationService
    {
        private readonly ILogger<NotificationService> _logger;
        private readonly IEmailService _emailService;
        private readonly IUserService _userService;
        private readonly IDiscountService _discountService;

        public NotificationService(
            ILogger<NotificationService> logger,
            IEmailService emailService,
            IUserService userService,
            IDiscountService discountService)
        {
            _logger = logger;
            _emailService = emailService;
            _userService = userService;
            _discountService = discountService;
        }

        public void Start()
        {
            RecurringJob.AddOrUpdate("SendEmails", () => SendEmailsAsync(),
                Cron.Weekly(DayOfWeek.Friday, 11), TimeZoneInfo.Local);
        }

        public async Task SendEmailsAsync()
        {
            await _emailService.SendEmailAsync("liza.pavliv@gmail.com",
                "News from HEH", "Hi! Here you can found some discounts");

            _logger.LogInformation("Emails where send");
        }

        public IEnumerable<string> GetUsersEmails()
        {

        }

        public IEnumerable<string> GetHotDiscounts()
        {
            var discounts = _discountService.GetHot();

            foreach (var discount in discounts)
            {
                
            }
        }

    }
}