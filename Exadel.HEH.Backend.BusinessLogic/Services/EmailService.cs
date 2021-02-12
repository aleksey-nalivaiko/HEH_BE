using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;

namespace Exadel.HEH.Backend.BusinessLogic.Services
{
    public class EmailService : IEmailService
    {
        // TODO: async / files?
        public void SendMail(string toEmailAddress, string emailTitle, string emailMsgBody)
        {
            MailAddress fromAddress = new MailAddress("team1.exadel@gmail.com", "Happy exadel hours");
            MailAddress toAddress = new MailAddress(toEmailAddress);
            MailMessage email = new MailMessage(fromAddress, toAddress);
            email.Subject = emailTitle;
            email.Body = emailMsgBody;
            email.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential("team1.exadel@gmail.com", "jwlwuqagfjbqcaeu");
            smtp.EnableSsl = true;
            try
            {
                smtp.Send(email);
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
