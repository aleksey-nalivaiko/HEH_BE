using System.Collections.Generic;

namespace Exadel.HEH.Backend.BusinessLogic.DTOs
{
    public class DiscountStatisticsDto : DiscountBaseDto
    {
        public IEnumerable<AddressDto> Addresses { get; set; }

        public IList<int> PhonesIds { get; set; }

        public int ViewsAmount { get; set; }
    }
}