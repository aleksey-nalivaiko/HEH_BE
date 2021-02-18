using System;
using System.Threading.Tasks;
using AutoMapper;
using Exadel.HEH.Backend.BusinessLogic.Extensions;
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

        public HistoryServiceTests()
        {
            var userRepository = new Mock<IUserRepository>();
            var userProvider = new Mock<IUserProvider>();
            var historyRepository = new Mock<IHistoryRepository>();

            _service = new HistoryService(userRepository.Object, Repository.Object, historyRepository.Object, MapperExtensions.Mapper, userProvider.Object);
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
        }

        [Fact]
        public async Task CanGetAll()
        {
            Data.Add(_history);
            var result = await _service.GetAllAsync();
            Assert.Single(result);
        }
    }
}