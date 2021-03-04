using System;
using System.Diagnostics.CodeAnalysis;

namespace Exadel.HEH.Backend.BusinessLogic.DTOs
{
    [ExcludeFromCodeCoverage]
    public class CityDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
