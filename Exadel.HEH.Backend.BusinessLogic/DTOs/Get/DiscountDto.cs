using System.Collections.Generic;

namespace Exadel.HEH.Backend.BusinessLogic.DTOs.Get
{
    public class DiscountDto : DiscountBaseDto
    {
        public IList<int> AddressesIds { get; set; }

        public IList<int> PhonesIds { get; set; }
    }
}