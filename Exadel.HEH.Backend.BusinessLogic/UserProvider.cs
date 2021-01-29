using System;

namespace Exadel.HEH.Backend.BusinessLogic
{
    public class UserProvider : IUserProvider
    {
        public Guid GetUserId()
        {
            return Guid.Parse("6dead3f8-599e-11eb-ae93-0242ac130002");
        }
    }
}