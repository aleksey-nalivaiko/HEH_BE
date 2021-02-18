using System.Threading.Tasks;

namespace Exadel.HEH.Backend.Host.Identity
{
    public interface IIdentityService
    {
        Task InitializeAsync();
    }
}