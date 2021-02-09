using System;
using System.Collections.Generic;

namespace Exadel.HEH.Backend.BusinessLogic.DTOs.Get
{
    public class DiscountDto : DiscountBaseDto
    {
        public IList<Guid> AddressesIds { get; set; }

        public IList<Guid> PhonesIds { get; set; }
    }
}