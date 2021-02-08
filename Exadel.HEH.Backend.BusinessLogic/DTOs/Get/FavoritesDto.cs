namespace Exadel.HEH.Backend.BusinessLogic.DTOs.Get
{
    public class FavoritesDto : DiscountDto
    {
        public string Note { get; set; }

        public override bool IsFavorite => true;
    }
}