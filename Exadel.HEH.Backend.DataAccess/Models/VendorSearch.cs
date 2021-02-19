using System;
using System.Collections.Generic;

namespace Exadel.HEH.Backend.DataAccess.Models
{
    public class VendorSearch : IDataModel
    {
        public Guid Id { get; set; }

        public string Vendor { get; set; }

        public IList<string> Discounts { get; set; }

        public IList<string> Categories { get; set; }

        public IList<string> Tags { get; set; }

        public IList<string> Countries { get; set; }

        public IList<string> Cities { get; set; }

        public IList<string> Streets { get; set; }

        public IList<Guid> CategoriesIds { get; set; }

        public IList<Guid> TagsIds { get; set; }

        public IList<Address> Addresses { get; set; }
    }
}