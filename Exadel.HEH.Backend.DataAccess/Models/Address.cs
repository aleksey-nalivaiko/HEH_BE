using System;

namespace Exadel.HEH.Backend.DataAccess.Models
{
    public class Address
    {
        public Guid Id { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Street { get; set; }
    }
}