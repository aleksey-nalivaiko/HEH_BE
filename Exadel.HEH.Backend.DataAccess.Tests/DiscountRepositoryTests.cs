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

        public DiscountRepositoryTests()
        {
            _repository = new DiscountRepository(Context.Object);

            var address = new Address
            {
                Id = Guid.NewGuid(),
                CountryId = "CountryId 1",
                CityId = "CityId 1",
                Street = "Street 1"
            };

            _discount = new Discount
            {
                Id = Guid.NewGuid(),
                Conditions = "new condition string",
                TagsIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() },
                VendorId = Guid.NewGuid(),
                PromoCode = "new promo code",
                Addresses = new List<Address>
                {
                    address,
                    new Address
                    {
                        Id = System.Guid.NewGuid(),
                        CountryId = "country 1",
                        CityId = "city 1",
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