using System;

namespace Exadel.HEH.Backend.BusinessLogic.Providers
{
    public interface ITimezoneProvider
    {
        int GetDateTimeOffset();
    }
}