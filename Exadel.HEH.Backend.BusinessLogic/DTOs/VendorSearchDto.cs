using System;
using System.Collections.Generic;

namespace Exadel.HEH.Backend.BusinessLogic.DTOs
{
    public class VendorSearchDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public IList<Guid> CategoriesIds { get; set; }

        public IList<Guid> TagsIds { get; set; }

        public IEnumerable<AddressDto> Addresses { get; set; }
    }
}