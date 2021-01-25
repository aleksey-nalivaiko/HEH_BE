using System;

namespace Exadel.HEH.Backend.DataAccess.Models
{
    public class Category : IDataModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
