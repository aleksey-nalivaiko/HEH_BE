using System;
using System.Collections.Generic;

namespace Exadel.HEH.Backend.BusinessLogic.DTOs
{
    public class VendorSearchDto
    {
        public Guid Id { get; set; }

        public string Vendor { get; set; }

        public IList<string> Discounts { get; set; }

        public IList<string> Categories { get; set; }

        public IList<string> Tags { get; set; }

        public IList<string> Countries { get; set; }

        public IList<string> Cities { get; set; }

        public IList<string> Streets { get; set; }

        public IList<Guid> CategoryIds { get; set; }

        public IList<Guid> TagIds { get; set; }

        public IEnumerable<AddressDto> Addresses { get; set; }
    }
}