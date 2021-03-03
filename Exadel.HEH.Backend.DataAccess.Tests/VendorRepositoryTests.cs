using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Extensions;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;
using Xunit;

namespace Exadel.HEH.Backend.DataAccess.Tests
{
    public class VendorRepositoryTests : BaseRepositoryTests<Vendor>
    {
        private readonly IVendorRepository _repository;
        private readonly Vendor _vendor;

        public VendorRepositoryTests()
        {
            _repository = new VendorRepository(Context.Object);
            _vendor = new Vendor
            {
                Id = Guid.NewGuid(),
                Name = "Test name",
                Email = "Test email",
                Addresses = new List<Address>(),
                Links = new List<Link>(),
                Phones = new List<Phone>(),
                WorkingHours = "10:00-24:00"
            };
        }

        [Fact]
        public async Task CanGetAll()
        {
            Collection.Add(_vendor);
            var result = await _repository.Get().ToListAsync();
            Assert.Single(result);
        }
    }
}