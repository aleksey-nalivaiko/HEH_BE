using System.Diagnostics.CodeAnalysis;

namespace Exadel.HEH.Backend.BusinessLogic.Options
{
    [ExcludeFromCodeCoverage]
    public class EmailOptions
    {
        public string Password { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }
    }
}
