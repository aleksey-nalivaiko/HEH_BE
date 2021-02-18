using Exadel.HEH.Backend.BusinessLogic.Providers;
using Microsoft.AspNetCore.Http;

namespace Exadel.HEH.Backend.Host.Infrastructure
{
    public class MethodProvider : IMethodProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MethodProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetMethodUpperName()
        {
            return _httpContextAccessor.HttpContext.Request.Method.ToUpper();
        }
    }
}
