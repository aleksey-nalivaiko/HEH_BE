using System;
using System.Linq;
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

        public int GetDateTimeOffset()
        {
            var offset = int.Parse(_httpContextAccessor.HttpContext.Request.Query["offset"]);

            return offset;
        }
    }
}