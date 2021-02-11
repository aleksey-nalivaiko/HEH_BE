using System;

namespace Exadel.HEH.Backend.DataAccess.Models
{
    public class Address
    {
        public int Id { get; set; }

        public Guid CountryId { get; set; }

        public Guid CityId { get; set; }

        public string Street { get; set; }
    }
}