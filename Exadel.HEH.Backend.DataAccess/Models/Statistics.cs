using System;

namespace Exadel.HEH.Backend.DataAccess.Models
{
    public class Statistics : IDataModel
    {
        public Guid Id { get; set; }

        public Guid DiscountId { get; set; }

        public DateTime DateTime { get; set; }

        public int ViewsAmount { get; set; }
    }
}