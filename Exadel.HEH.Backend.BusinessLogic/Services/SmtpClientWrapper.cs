using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;

namespace Exadel.HEH.Backend.BusinessLogic.Services
{
    public class SmtpClientWrapper : ISmtpClientWrapper
    {
        public async Task SendMailAsync(MailMessage message, string email, string password)
        {
            using var smtpClient = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(email, password),
                EnableSsl = true
            };

            await smtpClient.SendMailAsync(message);
        }
    }
}