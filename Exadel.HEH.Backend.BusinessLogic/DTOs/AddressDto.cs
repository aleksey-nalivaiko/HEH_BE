using System;

namespace Exadel.HEH.Backend.BusinessLogic.DTOs
{
    public class AddressDto
    {
        public int Id { get; set; }

        public Guid CountryId { get; set; }

        public Guid? CityId { get; set; }

        public string Street { get; set; }
    }
}