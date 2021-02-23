using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Models;

namespace Exadel.HEH.Backend.BusinessLogic.Services.Abstract
{
    public interface IEmailService
    {
        Task SendEmailAsync(string address, string subject, string messageBody);

        string CompleteHotNotificationMessage(
            Notification notification,
            string userName);
    }
}