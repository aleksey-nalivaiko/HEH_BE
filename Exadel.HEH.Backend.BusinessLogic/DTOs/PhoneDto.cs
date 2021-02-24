using System.ComponentModel.DataAnnotations;

namespace Exadel.HEH.Backend.BusinessLogic.DTOs
{
    public class PhoneDto
    {
        public int Id { get; set; }

        [Phone]
        public string Number { get; set; }
    }
}
