using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.ValidationServices;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;
using Moq;
using Xunit;

namespace Exadel.HEH.Backend.BusinessLogic.Tests.ValidationServicesTests
{
    public class UserValidationServiceTests
    {
        private readonly UserValidationService _validationService;
        private readonly User _user;

        public UserValidationServiceTests()
        {
            var userRepository = new Mock<IUserRepository>();
            var userData = new List<User>();

            _user = new User
            {
                Id = Guid.NewGuid()
            };
            userData.Add(_user);

            userRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .Returns((Guid id) => Task.FromResult(userData.FirstOrDefault(u => u.Id == id)));

            _validationService = new UserValidationService(userRepository.Object);
        }

        [Fact]
        public async Task CanValidateUserExists()
        {
            Assert.True(await _validationService.UserExists(_user.Id));
        }
    }
}