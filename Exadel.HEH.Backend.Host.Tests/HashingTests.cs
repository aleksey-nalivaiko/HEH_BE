using Exadel.HEH.Backend.Host.Identity.Security;
using Xunit;

namespace Exadel.HEH.Backend.Host.Tests
{
    public class HashingTests
    {
        [Fact]
        public void HashPasswordWithSalt()
        {
            var salt = Hashing.GetRandomSalt();
            var password = string.Empty;
            var hash = Hashing.HashPasswordWithSalt(password, salt);
        }
    }
}