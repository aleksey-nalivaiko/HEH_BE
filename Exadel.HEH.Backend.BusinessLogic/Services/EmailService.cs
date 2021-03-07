using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.Options;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Microsoft.Extensions.Options;
using Mustache;

namespace Exadel.HEH.Backend.BusinessLogic.Services
{
    public class EmailService : IEmailService
    {
        private const string EmailTemplatesPath = "EmailTemplates";
        private readonly EmailOptions _options;
        private readonly ISmtpClientWrapper _smtpClient;

        public EmailService(IOptions<EmailOptions> options, ISmtpClientWrapper smtpClient)
        {
            _options = options.Value;
            _smtpClient = smtpClient;
        }

        public async Task SendEmailAsync(string address, string subject, string messageBody)
        {
            var fromAddress = new MailAddress(_options.Email, _options.Name);
            var toAddress = new MailAddress(address);

            using var email = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = messageBody,
                IsBodyHtml = true
            };

            await _smtpClient.SendMailAsync(email, _options.Email, _options.Password);
        }

        public string CompleteHotNotificationsMessage(
            IEnumerable<Notification> notifications,
            string userName)
        {
            var generator = GetGenerator("hotEmail.html");

            return generator.Render(new
            {
                UserName = userName,
                Notifications = notifications
            });
        }

        public string CompleteNotificationsCountMessage(
            int count,
            string userName)
        {
            var generator = GetGenerator("countEmail.html");

            return generator.Render(new
            {
                UserName = userName,
                Count = count
            });
        }

        private Generator GetGenerator(string fileName)
        {
            var path = Path.Combine(EmailTemplatesPath, fileName);

            var compiler = new FormatCompiler();

            using var streamReader = new StreamReader(path, Encoding.UTF8);

            var generator = compiler.Compile(streamReader.ReadToEnd());
            return generator;
        }
    }
}
