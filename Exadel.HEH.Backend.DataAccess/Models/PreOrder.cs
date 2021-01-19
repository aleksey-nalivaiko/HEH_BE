using System;

namespace Exadel.Example.Models
{
    public class PreOrder
    {
        public int PreOrderId { get; set; }

        public int UserId { get; set; }

        public int DiscountId { get; set; }

        public DateTime OrderTime { get; set; }

        public string Info { get; set; }
    }
}
