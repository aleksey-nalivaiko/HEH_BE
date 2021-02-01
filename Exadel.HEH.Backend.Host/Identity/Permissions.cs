using Exadel.HEH.Backend.DataAccess.Models;

namespace Exadel.HEH.Backend.Host.Identity
{
    public static class Permissions
    {
        public const string Moderation = Moderator + "," + Administrator;
        public const string Administration = Administrator;

        private const string Moderator = nameof(UserRole.Moderator);
        private const string Administrator = nameof(UserRole.Administrator);
    }
}