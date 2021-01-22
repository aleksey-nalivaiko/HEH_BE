using AutoMapper;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.Host.DTOs.Create;
using Exadel.HEH.Backend.Host.DTOs.Get;

namespace Exadel.HEH.Backend.Host.Mappings
{
    public class HistoryProfile : Profile
    {
        public HistoryProfile()
        {
            CreateMap<History, HistoryDto>();

            CreateMap<HistoryCreateDto, History>();
        }
    }
}