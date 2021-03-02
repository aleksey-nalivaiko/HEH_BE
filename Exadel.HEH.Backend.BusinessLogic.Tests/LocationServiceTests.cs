using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.Services;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;
using Moq;
using Xunit;

namespace Exadel.HEH.Backend.BusinessLogic.Tests
{
    public class LocationServiceTests : BaseServiceTests<Location>
    {
        private readonly LocationService _service;
        private readonly Location _location;

        public LocationServiceTests()
        {
            var locationRepository = new Mock<ILocationRepository>();
            _service = new LocationService(locationRepository.Object, Mapper);

            _location = new Location
            {
                Id = Guid.NewGuid(),
                Country = "CountryName",
                Cities = new List<City>
                {
                    new City
                    {
                        Id = Guid.NewGuid(),
                        Name = "CityName"
                    }
                }
            };

            locationRepository.Setup(r => r.GetAllAsync())
                .Returns(Task.FromResult((IEnumerable<Location>)Data));

            locationRepository.Setup(r => r.GetByIdsAsync(It.IsAny<IEnumerable<Guid>>()))
                .Returns((IEnumerable<Guid> ids) => Task.FromResult(Data.Where(l => ids.Contains(l.Id))));
        }

        [Fact]
        public async Task CanGetAllAsync()
        {
            Data.Add(_location);
            var result = await _service.GetAllAsync();
            Assert.Single(result);
        }

        [Fact]
        public async Task CanGetByIdsAsync()
        {
            Data.Add(_location);

            var location = new Location
            {
                Id = Guid.NewGuid(),
                Country = "Country",
                Cities = new List<City>
                {
                    new City
                    {
                        Id = Guid.NewGuid(),
                        Name = "City"
                    }
                }
            };

            Data.Add(location);

            var result = await _service.GetByIdsAsync(new List<Guid> { _location.Id, location.Id });
            Assert.Equal(2, result.Count());
        }
    }
}
