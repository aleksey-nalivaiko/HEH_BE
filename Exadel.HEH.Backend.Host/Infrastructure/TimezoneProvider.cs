using System;
using Exadel.HEH.Backend.BusinessLogic.Providers;
using Microsoft.AspNetCore.Http;

namespace Exadel.HEH.Backend.Host.Infrastructure
{
    public class TimezoneProvider : ITimezoneProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TimezoneProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public DateTimeOffset GetDateTimeOffset()
        {
            //TODO: get offset from _httpContextAccessor
            throw new NotImplementedException();
        }
    }
}