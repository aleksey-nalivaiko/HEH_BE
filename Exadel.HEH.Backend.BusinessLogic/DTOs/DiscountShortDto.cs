using System.Collections.Generic;

namespace Exadel.HEH.Backend.BusinessLogic.DTOs
{
    public class DiscountShortDto : DiscountBaseDto
    {
        public IList<int> AddressesIds { get; set; }

        public IList<int> PhonesIds { get; set; }
    }
}