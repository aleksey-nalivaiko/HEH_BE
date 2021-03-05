namespace Exadel.HEH.Backend.BusinessLogic.DTOs
{
    public class FavoritesDto : DiscountDto
    {
        public string Note { get; set; }

        public override bool IsFavorite => true;
    }
}