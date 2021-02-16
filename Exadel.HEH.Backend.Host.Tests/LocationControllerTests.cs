using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.Host.Controllers;
using Moq;
using Xunit;

namespace Exadel.HEH.Backend.Host.Tests
{
    public class LocationControllerTests : BaseControllerTests<LocationDto>
    {
        private readonly LocationController _controller;
        private readonly LocationDto _location;

        public LocationControllerTests()
        {
            var locationService = new Mock<ILocationService>();

            _controller = new LocationController(locationService.Object);
            _location = new LocationDto
            {
                Id = Guid.NewGuid(),
                Country = "CountryName",
                Cities = new List<CityDto>
                {
                    new CityDto
                    {
                        Id = Guid.NewGuid(),
                        Name = "CityName"
                    }
                }
            };

            locationService.Setup(s => s.GetAllAsync())
                .Returns(() => Task.FromResult((IEnumerable<LocationDto>)Data));
        }

        [Fact]
        public async Task CanGetAll()
        {
            Data.Add(_location);
            var result = await _controller.GetAllAsync();
            Assert.Single(result);
        }
    }
}
