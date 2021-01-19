using System;

namespace Exadel.HEH.Backend.DataAccess.Models
{
    public class Tag
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int CategoryId { get; set; }
    }
}
