using System.Diagnostics.CodeAnalysis;
using Hangfire.Dashboard;

namespace Exadel.HEH.Backend.Host.Infrastructure
{
    [ExcludeFromCodeCoverage]
    public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            return true;
        }
    }
}