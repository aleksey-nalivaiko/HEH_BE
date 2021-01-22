using AutoMapper;
using Exadel.HEH.Backend.BusinessLogic.Mappings;

namespace Exadel.HEH.Backend.BusinessLogic
{
    public static class MapperExtensions
    {
        static MapperExtensions()
        {
            Configuration = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new DiscountProfile());
            });
            Mapper = Configuration.CreateMapper();
        }

        public static MapperConfiguration Configuration { get; }

        public static IMapper Mapper { get; }
    }
}