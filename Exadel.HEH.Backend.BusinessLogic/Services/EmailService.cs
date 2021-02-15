using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
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

        public async Task SendMail(string toEmailAddress, string emailTitle, string emailMsgBody)
        {
            MailAddress fromAddress = new MailAddress(_options.Email, _options.Name);
            MailAddress toAddress = new MailAddress(toEmailAddress);
            MailMessage email = new MailMessage(fromAddress, toAddress);
            email.Subject = emailTitle;
            email.Body = emailMsgBody;
            email.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential(_options.Email, _options.Password);
            smtp.EnableSsl = true;
            try
            {
                await smtp.SendMailAsync(email);
            }
            catch (SmtpFailedRecipientsException ex)
            {
                for (int i = 0; i < ex.InnerExceptions.Length; i++)
                {
                    SmtpStatusCode status = ex.InnerExceptions[i].StatusCode;
                    if (status == SmtpStatusCode.MailboxBusy ||
                        status == SmtpStatusCode.MailboxUnavailable)
                    {
                        System.Threading.Thread.Sleep(5000);
                        smtp.Send(email);
                    }
                }
            }
        }
    }
}
