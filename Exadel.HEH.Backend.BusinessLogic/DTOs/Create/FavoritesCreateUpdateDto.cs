using System;
using System.Collections.Generic;
using System.Text;

namespace Exadel.HEH.Backend.BusinessLogic.DTOs.Create
{
    public class FavoritesCreateUpdateDto
    {
        public Guid DiscountId { get; set; }

        public string Note { get; set; }
    }
}
