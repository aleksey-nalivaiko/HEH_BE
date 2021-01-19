using System;

namespace Exadel.HEH.Backend.DataAccess.Models
{
    public class PreOrder
    {
        public Guid Id { get; set; }

        public int UserId { get; set; }

        public int DiscountId { get; set; }

        public DateTime OrderTime { get; set; }

        public string Info { get; set; }
    }
}
