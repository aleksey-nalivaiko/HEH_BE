using System;

namespace Exadel.HEH.Backend.BusinessLogic.DTOs.Get
{
    public class TagDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid CategoryId { get; set; }
    }
}