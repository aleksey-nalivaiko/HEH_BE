using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.Options;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Microsoft.Extensions.Options;

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
    }
}
