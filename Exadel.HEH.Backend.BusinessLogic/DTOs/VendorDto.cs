using System.Collections.Generic;

namespace Exadel.HEH.Backend.BusinessLogic.DTOs
{
    public class VendorDto : VendorShortDto
    {
        public IEnumerable<LinkDto> Links { get; set; }

        public IEnumerable<PhoneDto> Phones { get; set; }

        public IEnumerable<AddressDto> Addresses { get; set; }

        public string Email { get; set; }

        public IEnumerable<DiscountShortDto> Discounts { get; set; }

        public string WorkingHours { get; set; }
    }
}