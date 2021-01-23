using System;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.Services;
using Exadel.HEH.Backend.DataAccess.Models;
using Xunit;

namespace Exadel.HEH.Backend.BusinessLogic.Tests
{
    public class HistoryServiceTests : BaseServiceTests<History>
    {
        private readonly HistoryService _service;
        private readonly History _history;

        public HistoryServiceTests()
        {
            _service = new HistoryService(Repository.Object);
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

        [Fact]
        public async Task CanCreate()
        {
            await _service.CreateAsync(_history);
            var history = Data.FirstOrDefault(x => x.Id == _history.Id);
            Assert.NotNull(history);
        }
    }
}