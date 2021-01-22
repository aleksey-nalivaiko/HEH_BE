using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Castle.Core.Internal;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories;
using MongoDB.Driver;
using Xunit;

namespace Exadel.HEH.Backend.DataAccess.Tests
{
    public class DiscountRepositoryTests : MongoRepositoryTests<Discount>
    {
        private readonly DiscountRepository _repository;

        private readonly Discount _discount;

        private readonly Address _address;

        public DiscountRepositoryTests()
        {
            _repository = new DiscountRepository(Context.Object);

            _address = new Address
            {
                Id = Guid.NewGuid(),
                Country = "Country 1",
                City = "City 1",
                Street = "Street 1"
            };

            _discount = new Discount
            {
                Id = Guid.NewGuid(),
                Conditions = "new condition string",
                Tags = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() },
                VendorId = Guid.NewGuid(),
                PromoCode = "new promo code",
                Addresses = new List<Address>
                {
                    _address,
                    new Address
                    {
                        Id = System.Guid.NewGuid(),
                        Country = "country 1",
                        City = "city 1",
                        Street = "street 2"
                    },
                },
                Phones = new List<Phone>
                {
                    new Phone
                    {
                        Id = Guid.NewGuid(),
                        Number = "+375441111111"
                    },
                    new Phone
                    {
                        Id = Guid.NewGuid(),
                        Number = "+375442222222"
                    },
                },
                StartDate = new DateTime(2021, 1, 20),
                EndDate = new DateTime(2021, 1, 25),
                CategoryId = Guid.NewGuid()
            };
        }

        [Fact]
        public async Task CanGetByLocation()
        {
            Collection.Add(_discount);

            var result = await _repository.GetByLocationAsync(_address);

            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task CanGetByTagId()
        {
            Collection.Add(_discount);

            var result = await _repository.GetByTagAsync(_discount.Tags[0]);

            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task CanGetByVendorId()
        {
            Collection.Add(_discount);

            var result = await _repository.GetByVendorAsync(_discount.VendorId);

            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task CanGetByCategoryId()
        {
            Collection.Add(_discount);

            var result = await _repository.GetByCategoryAsync(_discount.CategoryId);

            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task CanGetAll()
        {
            Collection.Add(_discount);

            var result = await _repository.GetAllAsync();

            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task CanGetById()
        {
            Collection.Add(_discount);

            var result = await _repository.GetByIdAsync(_discount.Id);

            Assert.Equal(_discount, result);
        }

        [Fact]
        public async Task CanCreate()
        {
            await _repository.CreateAsync(_discount);

            var discount = Collection.FirstOrDefault(x => x.Id == _discount.Id);

            Assert.NotNull(discount);
        }

        [Fact]
        public async Task CanUpdate()
        {
            Collection.Add(_discount.DeepClone());

            _discount.PromoCode = "new promo code";

            await _repository.UpdateAsync(_discount);

            Assert.True(Collection.Single(x => x.Id == _discount.Id).PromoCode.Equals("new promo code"));
        }
    }
}