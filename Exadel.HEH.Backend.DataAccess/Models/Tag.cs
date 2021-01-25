using System;

namespace Exadel.HEH.Backend.DataAccess.Models
{
    public class Tag : IDataModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid CategoryId { get; set; }
    }
}