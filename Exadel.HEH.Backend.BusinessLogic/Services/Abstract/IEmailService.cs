using System;
using System.Collections.Generic;
using System.Text;

namespace Exadel.HEH.Backend.BusinessLogic.Services.Abstract
{
    public interface IEmailService
    {
        void SendMail(string toEmailAddress, string emailTitle, string emailMsgBody);
    }
}
