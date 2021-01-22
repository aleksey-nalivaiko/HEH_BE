using System;
using System.Collections.Generic;
using System.Text;

namespace Exadel.HEH.Backend.BusinessLogic.DTOs.Get
{
    public class LinkDto
    {
        public string Url { get; set; }

        public LinkTypeDto Type { get; set; }

        public Guid Id { get; set; }
    }
}
