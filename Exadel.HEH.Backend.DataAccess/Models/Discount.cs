using System;
using System.Collections.Generic;

namespace Exadel.HEH.Backend.DataAccess.Models
{
    public class Discount : IDataModel
    {
        public Guid Id { get; set; }

        public string Conditions { get; set; }

        public IList<Guid> TagsIds { get; set; }

        public Guid VendorId { get; set; }

        public string PromoCode { get; set; }

        public IList<Address> Addresses { get; set; }

        public IList<Phone> Phones { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public Guid CategoryId { get; set; }
    }
}