using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Exadel.HEH.Backend.BusinessLogic.Services.Abstract
{
    public interface IEmailService
    {
        Task SendMailAsync(string toEmailAddress, string emailTitle, string emailMsgBody);
    }
}
