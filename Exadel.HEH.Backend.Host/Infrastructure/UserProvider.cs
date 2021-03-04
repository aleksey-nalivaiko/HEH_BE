using System;
using System.Diagnostics.CodeAnalysis;
using Exadel.HEH.Backend.BusinessLogic.Providers;
using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Http;

namespace Exadel.HEH.Backend.Host.Infrastructure
{
    [ExcludeFromCodeCoverage]
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