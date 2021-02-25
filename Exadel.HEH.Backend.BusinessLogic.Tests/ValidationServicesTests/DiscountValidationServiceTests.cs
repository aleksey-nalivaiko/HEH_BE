using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.ValidationServices;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;
using Moq;
using Xunit;

namespace Exadel.HEH.Backend.BusinessLogic.Tests.ValidationServicesTests
{
    public class DiscountValidationServiceTests
    {
        private readonly DiscountValidationService _validationService;
        private readonly Discount _discount;

        public DiscountValidationServiceTests()
        {
            var discountRepository = new Mock<IDiscountRepository>();
            var dicountData = new List<Discount>();
            _discount = new Discount { Id = default(Guid) };
            dicountData.Add(_discount);

            discountRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .Returns((Guid id) => Task.FromResult(dicountData.FirstOrDefault(d => d.Id == id)));

            _validationService = new DiscountValidationService(discountRepository.Object);
        }

        [Fact]
        public async Task CanValidateDiscountExists()
        {
            Assert.True(await _validationService.DiscountExists(_discount.Id, CancellationToken.None));
        }

        [Fact]
        public async Task CanValidateDiscountNotExists()
        {
            Assert.True(await _validationService.DiscountNotExists(Guid.NewGuid(), CancellationToken.None));
        }
    }
}