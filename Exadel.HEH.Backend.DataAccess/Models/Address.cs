using System;
using System.Collections.Generic;
using System.Text;

namespace Exadel.HEH.Backend.DataAccess.Models
{
    public class Address
    {
        public string Country { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public Guid Id { get; set; }
    }
}
