using System.Collections.Generic;

namespace Exadel.HEH.Backend.BusinessLogic.DTOs
{
    public class DiscountExtendedDto : DiscountBaseDto
    {
        public IEnumerable<AddressDto> Addresses { get; set; }

        public IEnumerable<PhoneDto> Phones { get; set; }

        public IEnumerable<LinkDto> Links { get; set; }

        public string WorkingHours { get; set; }
    }
}