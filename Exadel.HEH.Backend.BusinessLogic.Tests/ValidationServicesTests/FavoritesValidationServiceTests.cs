using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.Providers;
using Exadel.HEH.Backend.BusinessLogic.ValidationServices;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;
using Moq;
using Xunit;

namespace Exadel.HEH.Backend.BusinessLogic.Tests.ValidationServicesTests
{
    public class FavoritesValidationServiceTests
    {
        private readonly FavoritesValidationService _validationService;
        private readonly Discount _discount;
        private readonly Mock<IMethodProvider> _methodProviderMock;

        public FavoritesValidationServiceTests()
        {
            var userRepository = new Mock<IUserRepository>();
            var userProvider = new Mock<IUserProvider>();
            var userFavorite = new Favorites();
            _methodProviderMock = new Mock<IMethodProvider>();

            _discount = new Discount { Id = Guid.NewGuid() };
            userFavorite.DiscountId = _discount.Id;
            var currentUser = new User
            {
                Favorites = new List<Favorites>
                {
                    userFavorite
                }
            };

            userRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .Returns((Guid id) => Task.FromResult(currentUser));

            _validationService = new FavoritesValidationService(userRepository.Object,
                userProvider.Object, _methodProviderMock.Object);
        }

        [Fact]
        public async Task CanValidateUserFavoritesNotExist()
        {
            _methodProviderMock.Setup(p => p.GetMethodUpperName())
                .Returns(() => "PUT");

            Assert.True(await _validationService.UserFavoritesNotExists(_discount.Id, CancellationToken.None));

            _methodProviderMock.Setup(p => p.GetMethodUpperName())
                .Returns(() => "POST");

            Assert.False(await _validationService.UserFavoritesNotExists(_discount.Id, CancellationToken.None));

            _methodProviderMock.Setup(p => p.GetMethodUpperName())
                .Returns(() => "GET");

            Assert.True(await _validationService.UserFavoritesNotExists(_discount.Id, CancellationToken.None));
        }
    }
}