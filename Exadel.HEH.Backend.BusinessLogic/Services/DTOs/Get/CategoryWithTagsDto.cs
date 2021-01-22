using System;
using System.Collections.Generic;

namespace Exadel.HEH.Backend.Host.DTOs.Get
{
    public class CategoryWithTagsDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<TagDto> Tags { get; set; }
    }
}