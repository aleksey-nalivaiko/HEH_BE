using System;
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
                Credentials = new NetworkCredential(_options.Email, _options.Password),
                EnableSsl = true
            };
            await smtpClient.SendMailAsync(email);
        }

        public string CompleteHotNotificationMessage(
            Notification notification,
            string userName)
        {
            var compiler = new FormatCompiler();

            //TODO: realPath
            using var streamReader = new StreamReader(
                @".\Path\To\My\File.Mustache", Encoding.UTF8);

            var generator = compiler.Compile(streamReader.ReadToEnd());

            return generator.Render("Bob");
        }

        private class EmailBody
        {
            private string userName;

            private List<Notification> notifications;
        }
    }
}
