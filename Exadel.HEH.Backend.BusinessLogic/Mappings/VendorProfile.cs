using AutoMapper;
using Exadel.HEH.Backend.BusinessLogic.DTOs;
using Exadel.HEH.Backend.DataAccess.Models;

namespace Exadel.HEH.Backend.BusinessLogic.Mappings
{
    public class VendorProfile : Profile
    {
        public VendorProfile()
        {
            CreateMap<Vendor, VendorShortDto>();

            CreateMap<Vendor, VendorDto>()
                .ForMember(dest => dest.Discounts, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<Link, LinkDto>()
                .ReverseMap();

            CreateMap<VendorSearch, VendorSearchDto>();
        }
    }
}
