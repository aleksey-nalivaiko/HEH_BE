using System;
using Exadel.HEH.Backend.DataAccess.Models;

namespace Exadel.HEH.Backend.BusinessLogic.DTOs.Create
{
    public class HistoryCreateDto
    {
        public Guid UserId { get; set; }

        public UserRole UserRole { get; set; }

        public string UserName { get; set; }

        public string UserEmail { get; set; }

        public UserAction Action { get; set; }

        public string Description { get; set; }

        public DateTime DateTime { get; set; }
    }
}