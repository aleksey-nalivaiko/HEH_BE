using AutoMapper;
using Exadel.HEH.Backend.BusinessLogic.DTOs;
using Exadel.HEH.Backend.DataAccess.Models;

namespace Exadel.HEH.Backend.BusinessLogic.Mappings
{
    public class NotificationProfile : Profile
    {
        public NotificationProfile()
        {
            CreateMap<Notification, NotificationDto>();
        }
    }
}