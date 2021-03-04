using System.Diagnostics.CodeAnalysis;

namespace Exadel.HEH.Backend.BusinessLogic.Options
{
    [ExcludeFromCodeCoverage]
    public class NotificationOptions
    {
        public int HotDiscountDaysLeft { get; set; }

        public int HotDiscountWeekendDaysLeft { get; set; }
    }
}