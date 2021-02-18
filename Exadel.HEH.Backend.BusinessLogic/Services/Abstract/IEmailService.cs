using System.Threading.Tasks;

namespace Exadel.HEH.Backend.BusinessLogic.Services.Abstract
{
    public interface IEmailService
    {
        Task SendEmailAsync(string address, string subject, string messageBody);
    }
}