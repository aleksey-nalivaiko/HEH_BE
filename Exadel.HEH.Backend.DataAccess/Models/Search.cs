using System;
using System.Collections.Generic;

namespace Exadel.HEH.Backend.DataAccess.Models
{
    public class Search : IDataModel
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