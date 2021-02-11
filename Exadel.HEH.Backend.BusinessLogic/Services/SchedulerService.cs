using System;
using Hangfire;
using Microsoft.Extensions.Logging;

namespace Exadel.HEH.Backend.BusinessLogic.Services
{
    public class SchedulerService
    {
        private readonly ILogger<SchedulerService> _logger;

        public SchedulerService(ILogger<SchedulerService> logger)
        {
            _logger = logger;
        }

        public void Start()
        {
            RecurringJob.AddOrUpdate("SendEmails", () => SendEmails(),
                Cron.Weekly(DayOfWeek.Friday, 11), TimeZoneInfo.Local);
        }

        public void SendEmails()
        {
            //TODO: Call email service
            _logger.LogInformation("Emails where send");
        }
    }
}