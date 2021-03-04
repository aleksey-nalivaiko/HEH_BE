using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Exadel.HEH.Backend.DataAccess.Models
{
    [ExcludeFromCodeCoverage]
    public class DiscountSearch : IDataModel
    {
        public Guid Id { get; set; }

        public string Discount { get; set; }

        public string Vendor { get; set; }

        public string Category { get; set; }

        public IList<string> Tags { get; set; }

        public IList<string> Countries { get; set; }

        public IList<string> Cities { get; set; }

        public IList<string> Streets { get; set; }
    }
}