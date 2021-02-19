using System;

namespace Exadel.HEH.Backend.BusinessLogic.DTOs
{
    public class StatisticsDto
    {
        public Guid Id { get; set; }

        public Guid DiscountId { get; set; }

        public DateTime DateTime { get; set; }

        public int ViewsAmount { get; set; }
    }
}