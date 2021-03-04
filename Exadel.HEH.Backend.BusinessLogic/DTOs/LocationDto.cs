using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Exadel.HEH.Backend.BusinessLogic.DTOs
{
    [ExcludeFromCodeCoverage]
    public class LocationDto
    {
        public Guid Id { get; set; }

        public string Country { get; set; }

        public IList<CityDto> Cities { get; set; }
    }
}
