using System;

namespace Exadel.HEH.Backend.DataAccess.Models
{
    public class Search : IDataModel
    {
        public Guid Id { get; set; }

        public string Discount { get; set; }

        public string Vendor { get; set; }

        public string Category { get; set; }

        public string Tag { get; set; }

        public Location Location { get; set; }
    }
}