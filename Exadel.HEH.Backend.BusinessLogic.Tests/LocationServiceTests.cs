using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.Services;
using Exadel.HEH.Backend.DataAccess.Models;
using Xunit;

namespace Exadel.HEH.Backend.BusinessLogic.Tests
{
    public class LocationServiceTests : BaseServiceTests<Location>
    {
        private readonly LocationService _service;
        private readonly Location _location;

        public LocationServiceTests()
        {
            _service = new LocationService(Repository.Object, Mapper);

            _location = new Location
            {
                Id = Guid.NewGuid(),
                Country = "CountryName"
            };
        }

        [Fact]
        public async Task CanGetAll()
        {
            Data.Add(_location);
            var result = await _service.GetAllAsync();
            Assert.Single(result);
        }
    }
}
