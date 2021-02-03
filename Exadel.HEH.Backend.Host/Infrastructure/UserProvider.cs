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
            return Guid.Parse(_httpContextAccessor.HttpContext.User.GetSubjectId());
        }
    }
}