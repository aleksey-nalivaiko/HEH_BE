using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;
using IdentityModel;
using IdentityServer4.Validation;

namespace Exadel.HEH.Backend.Host.Identity
{
    public class UserResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IUserRepository _userRepository;

        public UserResourceOwnerPasswordValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var user = await _userRepository.GetByEmail(context.UserName);
            if (user.Password == context.Password)
            {
               context.Result = new GrantValidationResult(user.Id.ToString(),
                   OidcConstants.AuthenticationMethods.Password, null);
            }
        }
    }
}