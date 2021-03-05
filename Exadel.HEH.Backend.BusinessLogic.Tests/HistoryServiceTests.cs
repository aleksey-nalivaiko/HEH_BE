using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.Extensions;
using Exadel.HEH.Backend.BusinessLogic.Providers;
using Exadel.HEH.Backend.BusinessLogic.Services;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;
using Moq;
using Xunit;

namespace Exadel.HEH.Backend.BusinessLogic.Tests
{
    public class HistoryServiceTests : BaseServiceTests<History>
    {
        private readonly HistoryService _service;
        private readonly History _history;
        private readonly List<History> _data;

        public HistoryServiceTests()
        {
            var userRepository = new Mock<IUserRepository>();
            var userProvider = new Mock<IUserProvider>();
            var historyRepository = new Mock<IHistoryRepository>();

            _data = new List<History>();
            _service = new HistoryService(userRepository.Object, historyRepository.Object,
                MapperExtensions.Mapper, userProvider.Object);
            _history = new History
            {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Action = UserAction.Add,
                Description = "Added",
                DateTime = DateTime.Now,
                UserEmail = "email@mail.com",
                UserName = "Mary",
                UserRole = UserRole.Moderator
            };
            var currentUser = new User
            {
                Id = Guid.NewGuid(),
                Email = "testEmail@gmail.com",
                Name = "Test name",
                Role = UserRole.Moderator,
                Address = new Address
                {
                    CountryId = Guid.NewGuid(),
                    CityId = Guid.NewGuid(),
                    Street = "Test street"
                }
            };

            historyRepository.Setup(r => r.Get()).Returns(Data.AsQueryable());
            historyRepository.Setup(r => r.CreateAsync(It.IsAny<History>()))
                .Callback((History item) => { _data.Add(item); })
                .Returns(Task.CompletedTask);
            userProvider.Setup(p => p.GetUserId()).Returns(currentUser.Id);
            userRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .Returns(() => Task.FromResult(currentUser));
        }

        [Fact]
        public void CanGetAll()
        {
            Data.Add(_history);
            var result = _service.GetAllAsync().IsCompletedSuccessfully;
            Assert.True(result);
        }

        [Fact]
        public async Task CanCreate()
        {
            await _service.CreateAsync(UserAction.Add, "Some description");
            Assert.Single(_data);
        }
    }
}