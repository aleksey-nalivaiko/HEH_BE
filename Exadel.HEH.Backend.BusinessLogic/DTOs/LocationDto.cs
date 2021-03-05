using System;
using System.Collections.Generic;

namespace Exadel.HEH.Backend.BusinessLogic.DTOs
{
    public class LocationDto
    {
        public Guid Id { get; set; }

        public string Country { get; set; }

        public IList<CityDto> Cities { get; set; }
    }
}
