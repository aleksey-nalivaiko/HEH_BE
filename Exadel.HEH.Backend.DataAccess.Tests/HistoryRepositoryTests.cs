using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories;
using MongoDB.Driver;
using Moq;
using Xunit;

namespace Exadel.HEH.Backend.DataAccess.Tests
{
    public class HistoryRepositoryTests : MongoRepositoryTests<History>
    {
        private readonly HistoryRepository _repository;
        private readonly History _history;

        public HistoryRepositoryTests()
        {
            _repository = new HistoryRepository(Database.Object);
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
            Collection.Setup(c => Task.FromResult(c.Find(Builders<History>.Filter.Empty, null)
                    .ToEnumerable(CancellationToken.None)))
                .Returns<Task<IEnumerable<History>>>(history => history)
                .Verifiable();

            await _repository.GetAllAsync();
        }

        [Fact]
        public async Task CanCreate()
        {
            Collection.Setup(c => c.InsertOneAsync(_history, null, CancellationToken.None))
                .Returns(Task.CompletedTask)
                .Verifiable();

            await _repository.CreateAsync(_history);
        }
    }
}