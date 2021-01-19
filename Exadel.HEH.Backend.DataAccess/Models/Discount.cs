using System;

namespace Exadel.HEH.Backend.DataAccess.Models
{
    public class Discount
    {
        public Guid Id { get; set; }

        public string Conditions { get; set; }

        public Tag[] Tags { get; set; }

        public Guid VendorId { get; set; }

        public string PromoCode { get; set; }

        public Address[] Addresses { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public Guid CategoryId { get; set; }
    }
}