using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Extensions;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories;
using Moq;
using Xunit;

namespace Exadel.HEH.Backend.DataAccess.Tests
{
    public class HistoryRepositoryTests : BaseRepositoryTests<History>
    {
        private readonly HistoryRepository _repository;
        private readonly History _history;
        private readonly List<History> _data;

        public HistoryRepositoryTests()
        {
            _data = new List<History>();
            _repository = new HistoryRepository(Context.Object);
            _history = new History
            {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Action = UserAction.Add,
                Description = "Added",
                DateTime = DateTime.Now,
                UserEmail = "email@mail.com",
                UserName = "Mary",
                UserRole = UserRole.Moderator,
                UserAddress = new Address
                {
                    Id = 1,
                    CountryId = Guid.NewGuid(),
                    CityId = Guid.NewGuid(),
                    Street = "A"
                }
            };

            Context.Setup(c => c.CreateAsync(It.IsAny<History>()))
                .Callback((History item) => _data.Add(item))
                .Returns(Task.CompletedTask);
            Context.Setup(c => c.GetAll<History>())
                .Returns(() => _data.AsQueryable());
        }

        [Fact]
        public async Task CanGetAll()
        {
            await _repository.CreateAsync(_history);
            var result = await _repository.GetAllAsync();
            Assert.Single(result);
        }

        [Fact]
        public async Task CanCreate()
        {
            await _repository.CreateAsync(_history);
            var history = _data.FirstOrDefault(x => x.Id == _history.Id);
            Assert.NotNull(history);
        }

        [Fact]
        public async Task CanGetAsQuery()
        {
            await _repository.CreateAsync(_history);
            var result = await _repository.Get().ToListAsync();
            Assert.Single(result);
        }
    }
}