using System.Net.Mail;
using System.Threading.Tasks;

namespace Exadel.HEH.Backend.BusinessLogic.Services.Abstract
{
    public interface ISmtpClientWrapper
    {
        Task SendMailAsync(MailMessage message, string email, string password);
    }
}