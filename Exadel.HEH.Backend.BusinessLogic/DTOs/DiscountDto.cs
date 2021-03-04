using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Exadel.HEH.Backend.BusinessLogic.DTOs
{
    [ExcludeFromCodeCoverage]
    public class DiscountDto : DiscountBaseDto
    {
        public IEnumerable<AddressDto> Addresses { get; set; }

        public IList<int> PhonesIds { get; set; }

        public virtual bool IsFavorite { get; set; }
    }
}