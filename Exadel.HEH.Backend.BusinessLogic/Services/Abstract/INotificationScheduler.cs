using System.Threading.Tasks;

namespace Exadel.HEH.Backend.BusinessLogic.Services.Abstract
{
    public interface INotificationScheduler
    {
        void StartJobs();

        Task SendHotNotificationsAsync();

        Task SendNotificationsCountAsync();
    }
}