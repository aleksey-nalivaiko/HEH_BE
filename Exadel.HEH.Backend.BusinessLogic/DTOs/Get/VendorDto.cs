using System.Collections.Generic;

namespace Exadel.HEH.Backend.BusinessLogic.DTOs.Get
{
    public class VendorDto : VendorShortDto
    {
        public IEnumerable<LinkDto> Links { get; set; }

        public bool Mailing { get; set; }

        public IEnumerable<PhoneDto> Phones { get; set; }

        public IEnumerable<AddressDto> Addresses { get; set; }

        public int ViewsAmount { get; set; }

        public string Email { get; set; }

        public IEnumerable<DiscountDto> Discounts { get; set; }

        public string WorkingHours { get; set; }
    }
}
