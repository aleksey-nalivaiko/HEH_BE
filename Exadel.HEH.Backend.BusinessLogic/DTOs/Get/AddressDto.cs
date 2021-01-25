using System;

namespace Exadel.HEH.Backend.BusinessLogic.DTOs.Get
{
    public class AddressDto
    {
        public Guid Id { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Street { get; set; }
    }
}