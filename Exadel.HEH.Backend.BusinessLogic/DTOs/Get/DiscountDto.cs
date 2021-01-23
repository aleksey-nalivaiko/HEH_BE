using System;
using System.Collections.Generic;

namespace Exadel.HEH.Backend.BusinessLogic.DTOs.Get
{
    public class DiscountDto
    {
        public Guid Id { get; set; }

        public string Conditions { get; set; }

        public IList<Guid> Tags { get; set; }

        public Guid VendorId { get; set; }

        public string PromoCode { get; set; }

        public IList<AddressDto> Addresses { get; set; }

        public IList<PhoneDto> Phones { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public Guid CategoryId { get; set; }
    }
}