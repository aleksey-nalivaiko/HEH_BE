using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;

namespace Exadel.HEH.Backend.Host.Identity
{
    [ExcludeFromCodeCoverage]
    public class UserProfileService : IProfileService
    {
        private readonly IUserRepository _userRepository;

        public UserProfileService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var user = await _userRepository.GetByIdAsync(Guid.Parse(context.Subject.GetSubjectId()));

            var roles = new List<string>();

            if (user.Role == UserRole.Employee)
            {
                roles.Add(nameof(UserRole.Employee));
            }

            if (user.Role == UserRole.Moderator
                || user.Role == UserRole.Administrator)
            {
                roles.Add(nameof(UserRole.Employee));
                roles.Add(user.Role.ToString());
            }

            if (user.Role == UserRole.Administrator)
            {
                roles.Add(nameof(UserRole.Moderator));
            }

            var claims = new List<Claim>
            {
                new Claim(JwtClaimTypes.Name, user.Name),
                new Claim(JwtClaimTypes.Email, user.Email),
                new Claim(JwtClaimTypes.Role, JsonSerializer.Serialize(roles),
                    IdentityServerConstants.ClaimValueTypes.Json)
            };

            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var user = await _userRepository.GetByIdAsync(Guid.Parse(context.Subject.GetSubjectId()));
            context.IsActive = user.IsActive;
        }
    }
}