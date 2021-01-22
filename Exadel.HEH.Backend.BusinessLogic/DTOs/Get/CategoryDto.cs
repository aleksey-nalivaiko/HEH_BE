using System;
using System.Collections.Generic;

namespace Exadel.HEH.Backend.BusinessLogic.DTOs.Get
{
    public class CategoryDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<TagDto> Tags { get; set; }
    }
}