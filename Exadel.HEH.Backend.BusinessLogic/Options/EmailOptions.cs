using System;
using System.Collections.Generic;
using System.Text;

namespace Exadel.HEH.Backend.BusinessLogic.Options
{
    public class EmailOptions
    {
        public const string EmailSettings = "EmailSettings";

        public string Password { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }
    }
}
