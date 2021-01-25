using System;
using System.Collections.Generic;

namespace Exadel.HEH.Backend.DataAccess.Models
{
    public class Location
    {
        public Guid Id { get; set; }

        public string Country { get; set; }

        public IList<City> Cities { get; set; }
    }
}