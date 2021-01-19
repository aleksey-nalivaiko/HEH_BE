using System;

namespace Exadel.HEH.Backend.DataAccess.Models
{
    public class Vendor
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Link[] Links { get; set; }

        public bool Mailing { get; set; }

        public string Phone { get; set; }

        public int ViewsAmount { get; set; }

        public string Email { get; set; }
    }
}