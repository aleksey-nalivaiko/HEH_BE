using System.Collections.Generic;

namespace Exadel.HEH.Backend.BusinessLogic.DTOs
{
    public class DiscountDto : DiscountBaseDto
    {
        public IEnumerable<AddressDto> Addresses { get; set; }

        public IList<int> PhonesIds { get; set; }

        public virtual bool IsFavorite { get; set; }
    }
}