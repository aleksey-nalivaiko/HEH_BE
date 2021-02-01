using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;
using IdentityModel;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;

namespace Exadel.HEH.Backend.Host.Identity
{
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
            var claims = new List<Claim>
            {
                new Claim(JwtClaimTypes.Role, user.Role.ToString()),
                new Claim(JwtClaimTypes.Name, user.Name),
                new Claim(JwtClaimTypes.Email, user.Email)
            };

            if (user.Role != UserRole.Employee)
            {
                claims.Add(new Claim(JwtClaimTypes.Role, nameof(UserRole.Employee)));
            }

            if (user.Role == UserRole.Administrator)
            {
                claims.Add(new Claim(JwtClaimTypes.Role, nameof(UserRole.Moderator)));
            }

            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var user = await _userRepository.GetByIdAsync(Guid.Parse(context.Subject.GetSubjectId()));
            context.IsActive = user.IsActive;
        }
    }
}