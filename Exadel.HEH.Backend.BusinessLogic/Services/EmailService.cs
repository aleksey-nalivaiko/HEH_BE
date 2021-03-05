using System.Collections.Generic;
using System.IO;
using System.Net;
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

        public EmailService(IOptions<EmailOptions> options)
        {
            _options = options.Value;
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

            using var smtpClient = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_options.Email, _options.Password),
                EnableSsl = true
            };

            await smtpClient.SendMailAsync(email);
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
