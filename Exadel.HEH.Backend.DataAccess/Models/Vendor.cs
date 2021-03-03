using System;
using System.Collections.Generic;

namespace Exadel.HEH.Backend.DataAccess.Models
{
    public class Vendor : IDataModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public IList<Link> Links { get; set; }

        public IList<Phone> Phones { get; set; }

        public IList<Address> Addresses { get; set; }

        public string Email { get; set; }

        public string WorkingHours { get; set; }
    }
}