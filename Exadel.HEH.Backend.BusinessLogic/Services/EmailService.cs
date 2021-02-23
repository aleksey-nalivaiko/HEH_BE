﻿using System.Collections.Generic;
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

            //TODO: return html
            using var email = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = messageBody /*,
                IsBodyHtml = true*/
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

        public string CompleteHotNotificationsMessage(
            IEnumerable<Notification> notifications,
            string userName)
        {
            //TODO: realPath
            var generator = GetGenerator(@"Pth/jj");

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
            //TODO: realPath
            var generator = GetGenerator(@"Pth/jj");

            return generator.Render(new
            {
                UserName = userName,
                Count = count
            });
        }

        private Generator GetGenerator(string path)
        {
            var compiler = new FormatCompiler();

            using var streamReader = new StreamReader(path, Encoding.UTF8);

            var generator = compiler.Compile(streamReader.ReadToEnd());
            return generator;
        }
    }
}
