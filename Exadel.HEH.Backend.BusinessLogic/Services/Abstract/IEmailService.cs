using System.Collections.Generic;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Models;

namespace Exadel.HEH.Backend.BusinessLogic.Services.Abstract
{
    public interface IEmailService
    {
        Task SendEmailAsync(string address, string subject, string messageBody);

        string CompleteHotNotificationsMessage(
            IEnumerable<Notification> notifications,
            string userName);

        string CompleteNotificationsCountMessage(
            int count,
            string userName);
    }
}