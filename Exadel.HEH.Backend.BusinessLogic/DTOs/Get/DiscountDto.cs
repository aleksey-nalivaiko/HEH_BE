using System;
using System.Collections.Generic;

namespace Exadel.HEH.Backend.BusinessLogic.DTOs.Get
{
    public class DiscountDto
    {
        public Guid Id { get; set; }

        public string Conditions { get; set; }

        public IList<Guid> TagsIds { get; set; }

        public Guid VendorId { get; set; }

        public string VendorName { get; set; }

        public string PromoCode { get; set; }

        public IEnumerable<AddressDto> Addresses { get; set; }

        public IEnumerable<PhoneDto> Phones { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public Guid CategoryId { get; set; }
    }
}