using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Extensions;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories;
using Xunit;

namespace Exadel.HEH.Backend.DataAccess.Tests
{
    public class DiscountRepositoryTests : BaseRepositoryTests<Discount>
    {
        private readonly DiscountRepository _repository;
        private Discount _discount;

        public DiscountRepositoryTests()
        {
            _repository = new DiscountRepository(Context.Object);
            InitTestData();
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
        public async Task CanGetByIds()
        {
            Collection.Add(_discount);
            var result = await _repository.GetByIdsAsync(new List<Guid> { _discount.Id });

            Assert.Single(result);
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

        [Fact]
        public async Task CanRemoveByVendorId()
        {
            Collection.Add(_discount);

            await _repository.RemoveAsync(d => d.VendorId == _discount.VendorId);

            Assert.Empty(Collection);
        }

        private void InitTestData()
        {
            _discount = new Discount
            {
                Id = Guid.NewGuid(),
                PhonesIds = new List<int>
                {
                    2
                },
                CategoryId = Guid.NewGuid(),
                Conditions = "Conditions",
                TagsIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() },
                VendorId = Guid.NewGuid(),
                VendorName = "Vendor",
                PromoCode = "new promo code",
                StartDate = new DateTime(2021, 1, 20),
                EndDate = new DateTime(2021, 1, 25)
            };
        }
    }
}