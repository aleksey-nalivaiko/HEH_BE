using System;

namespace Exadel.HEH.Backend.DataAccess.Models
{
    public class PreOrder : IDataModel
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid DiscountId { get; set; }

        public DateTime DateTime { get; set; }

        public string Info { get; set; }
    }
}
