using System;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories;
using Xunit;

namespace Exadel.HEH.Backend.DataAccess.Tests
{
    public class HistoryRepositoryTests : MongoRepositoryTests<History>
    {
        private readonly HistoryRepository _repository;
        private readonly History _history;

        public HistoryRepositoryTests()
        {
            _repository = new HistoryRepository(Context.Object);
            _history = new History
            {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Action = UserAction.Add,
                Description = "Added",
                ActionDateTime = DateTime.Now,
                UserEmail = "email@mail.com",
                UserName = "Mary",
                UserRole = UserRole.Moderator
            };
        }

        [Fact]
        public async Task CanGetAll()
        {
            Collection.Add(_history);

            var result = await _repository.GetAllAsync();
            Assert.Single(result);
        }

        [Fact]
        public async Task CanCreate()
        {
            await _repository.CreateAsync(_history);

            var history = Collection.FirstOrDefault(x => x.Id == _history.Id);

            Assert.NotNull(history);
        }
    }
}