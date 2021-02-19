using System;
using System.Linq;
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

        public HistoryServiceTests()
        {
            var userRepository = new Mock<IUserRepository>();
            var userProvider = new Mock<IUserProvider>();
            var historyRepository = new Mock<IHistoryRepository>();
            var timezoneProvider = new Mock<ITimezoneProvider>();

            _service = new HistoryService(userRepository.Object, historyRepository.Object,
                MapperExtensions.Mapper, userProvider.Object, timezoneProvider.Object);
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

            historyRepository.Setup(r => r.Get()).Returns(Data.AsQueryable());
        }

        //[Fact]
        //public void CanGetAll()
        //{
        //    Data.Add(_history);
        //    var result = _service.Get();
        //    Assert.Single(result);
        //}
    }
}