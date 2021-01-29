using System;

namespace Exadel.HEH.Backend.BusinessLogic
{
    public interface IUserProvider
    {
        Guid GetUserId();
    }
}