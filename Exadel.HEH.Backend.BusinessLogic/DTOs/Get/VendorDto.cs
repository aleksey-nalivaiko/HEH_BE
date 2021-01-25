using System;
using System.Collections.Generic;

namespace Exadel.HEH.Backend.BusinessLogic.DTOs.Get
{
    public class VendorDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public IList<LinkDto> Links { get; set; }

        public bool Mailing { get; set; }

        public IList<PhoneDto> Phones { get; set; }

        public IList<AddressDto> Addresses { get; set; }

        public int ViewsAmount { get; set; }

        public string Email { get; set; }
    }
}
