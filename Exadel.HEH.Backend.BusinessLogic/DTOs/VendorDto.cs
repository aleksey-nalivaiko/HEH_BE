using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Exadel.HEH.Backend.BusinessLogic.DTOs
{
    public class VendorDto : VendorShortDto
    {
        public IEnumerable<LinkDto> Links { get; set; }

        public bool Mailing { get; set; }

        public IEnumerable<PhoneDto> Phones { get; set; }

        public IEnumerable<AddressDto> Addresses { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public IEnumerable<DiscountShortDto> Discounts { get; set; }

        public string WorkingHours { get; set; }
    }
}