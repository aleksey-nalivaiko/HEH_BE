using System;

namespace Exadel.HEH.Backend.DataAccess.Models
{
    public class Link
    {
        public string Url { get; set; }

        public Enum Type { get; set; }

        public Guid Id { get; set; }
    }
}