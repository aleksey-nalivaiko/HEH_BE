using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Exadel.HEH.Backend.BusinessLogic.DTOs
{
    [ExcludeFromCodeCoverage]
    public class DiscountExtendedDto : DiscountBaseDto
    {
        public IEnumerable<AddressDto> Addresses { get; set; }

        public IEnumerable<PhoneDto> Phones { get; set; }

        public IEnumerable<LinkDto> Links { get; set; }

        public string WorkingHours { get; set; }

        public bool IsFavorite { get; set; }
    }
}