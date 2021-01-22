using System;
using System.Collections.Generic;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;

namespace Exadel.HEH.Backend.Host.DTOs.Get
{
    public class CategoryWithTagsDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<TagDto> Tags { get; set; }
    }
}