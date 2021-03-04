using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Exadel.HEH.Backend.BusinessLogic.DTOs
{
    [ExcludeFromCodeCoverage]
    public class VendorSearchDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public IList<Guid> CategoriesIds { get; set; }

        public IList<Guid> TagsIds { get; set; }

        public IEnumerable<AddressDto> Addresses { get; set; }
    }
}