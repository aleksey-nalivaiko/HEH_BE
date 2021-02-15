using System;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Hangfire;
using Microsoft.Extensions.Logging;

namespace Exadel.HEH.Backend.BusinessLogic.Services
{
    public class SchedulerService
    {
        private readonly ILogger<SchedulerService> _logger;
        private readonly IEmailService _emailService;

        public SchedulerService(ILogger<SchedulerService> logger, IEmailService emailService)
        {
            _logger = logger;
            _emailService = emailService;
        }

        public void Start()
        {
            RecurringJob.AddOrUpdate("SendEmails", () => SendEmailsAsync(),
                Cron.Weekly(DayOfWeek.Friday, 11), TimeZoneInfo.Local);
        }

        public async Task SendEmailsAsync()
        {
            //TODO: Call email service
            await _emailService.SendMailAsync("User@mail.ru", "News from HEH", "Hi! Here you can found some discounts");
            _logger.LogInformation("Emails where send");
        }
    }
}