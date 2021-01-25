using System;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Create;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.Host.Controllers;
using Xunit;

namespace Exadel.HEH.Backend.Host.Tests
{
    public class LocationControllerTests : BaseControllerTests<LocationDto>
    {
        private readonly LocationController _controller;
        private readonly LocationDto _location;

        public LocationControllerTests()
        {
            _controller = new LocationController(Service.Object);
            _location = new LocationDto
            {
                Id = Guid.NewGuid(),
                Country = "CountryName"
            };
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
