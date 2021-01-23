using AutoMapper;
using Exadel.HEH.Backend.BusinessLogic.Mappings;

namespace Exadel.HEH.Backend.BusinessLogic
{
    public static class MapperExtensions
    {
        static MapperExtensions()
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new UserProfile());
                mc.AddProfile(new HistoryProfile());
                mc.AddProfile(new VendorProfile());
                mc.AddProfile(new TagProfile());
                mc.AddProfile(new DiscountProfile());
                mc.AddProfile(new AddressProfile());
                mc.AddProfile(new PhoneProfile());
            });
            mapperConfig.AssertConfigurationIsValid();

            Mapper = mapperConfig.CreateMapper();
        }

        public static IMapper Mapper { get; }
    }
}