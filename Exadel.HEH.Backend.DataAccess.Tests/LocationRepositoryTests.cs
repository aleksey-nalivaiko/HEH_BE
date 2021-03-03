using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories;
using Xunit;

namespace Exadel.HEH.Backend.DataAccess.Tests
{
    public class LocationRepositoryTests : BaseRepositoryTests<Location>
    {
        private readonly LocationRepository _repository;

        private readonly Location _location;

        public LocationRepositoryTests()
        {
            _repository = new LocationRepository(Context.Object);
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
        }

        [Fact]
        public async Task CanGetAllAsync()
        {
            Collection.Add(_location);

            var result = await _repository.GetAllAsync();
            Assert.Single(result);
        }

        [Fact]
        public async Task CanGetByIdAsync()
        {
            Collection.Add(_location);

            var result = await _repository.GetByIdAsync(_location.Id);
            Assert.Equal(_location, result);
        }

        [Fact]
        public async Task CanGetByIdsAsync()
        {
            Collection.Add(_location);

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

            Collection.Add(location);

            var result = await _repository.GetByIdsAsync(new List<Guid> { _location.Id, location.Id });
            Assert.Equal(2, result.Count());
        }
    }
}