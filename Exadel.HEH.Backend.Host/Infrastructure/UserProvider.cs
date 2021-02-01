using System;
using Exadel.HEH.Backend.BusinessLogic;
using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Http;

namespace Exadel.HEH.Backend.Host.Infrastructure
{
    public class UserProvider : IUserProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid GetUserId()
        {
            var user = _httpContextAccessor.HttpContext.User;
            if (user.IsAuthenticated())
            {
                var subjectId = user.GetSubjectId();
                return Guid.Parse(subjectId);
            }

            // TODO: remove
            return Guid.Parse("6dead3f8-599e-11eb-ae93-0242ac130002");
        }
    }
}