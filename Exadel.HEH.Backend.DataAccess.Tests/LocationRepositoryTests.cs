using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories;
using Xunit;

namespace Exadel.HEH.Backend.DataAccess.Tests
{
    public class LocationRepositoryTests : MongoRepositoryTests<Location>
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
        public async Task CanGetAll()
        {
            Collection.Add(_location);

            var result = await _repository.GetAllAsync();
            Assert.Single(result);
        }

        [Fact]
        public async Task CanGetById()
        {
            Collection.Add(_location);

            var result = await _repository.GetByIdAsync(_location.Id);
            Assert.Equal(_location, result);
        }

        [Fact]
        public async Task CanUpdate()
        {
            Collection.Add(_location.DeepClone());
            _location.Country = "NewCountryName";

            await _repository.UpdateAsync(_location);
            Assert.Equal("NewCountryName", Collection.Single(x => x.Id == _location.Id).Country);
        }
    }
}