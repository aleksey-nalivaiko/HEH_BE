using System.Diagnostics.CodeAnalysis;

namespace Exadel.HEH.Backend.BusinessLogic.DTOs
{
    [ExcludeFromCodeCoverage]
    public class FavoritesDto : DiscountDto
    {
        public string Note { get; set; }

        public override bool IsFavorite => true;
    }
}