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
    public class UserRepositoryTests : MongoRepositoryTests<User>
    {
        private readonly UserRepository _repository;

        private readonly User _user;

        public UserRepositoryTests()
        {
            _repository = new UserRepository(Database.Object);
            _user = new User
            {
                Id = Guid.NewGuid(),
                CategoryNotificationsId = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() },
                CityChangeNotificationIsOn = true,
                Email = "abc@mail.com",
                Favorites = new List<Favorites>(),
                HotDiscountsNotificationIsOn = false,
                IsActive = true,
                Name = "Mary",
                NewDiscountNotificationIsOn = true,
                NewVendorNotificationIsOn = true,
                TagNotificationsId = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() },
                VendorNotificationsId = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() },
                Role = UserRole.Employee,
                Office = new Address
                {
                    City = "m",
                    Country = "b",
                    Street = "g"
                },
                Password = "abc"
            };
        }

        [Fact]
        public async Task CanGetAll()
        {
            Collection.Setup(c => Task.FromResult(c.Find(Builders<User>.Filter.Empty, null)
                    .ToEnumerable(CancellationToken.None)))
                .Returns<Task<IEnumerable<User>>>(users => users)
                .Verifiable();

            await _repository.GetAllAsync();
        }

        [Fact]
        public async Task CanGetById()
        {
            Collection.Setup(c => c.Find(Builders<User>.Filter.Eq(x => x.Id, _user.Id), null)
                    .FirstOrDefaultAsync(CancellationToken.None))
                .Returns<Task<User>>(user => user)
                .Verifiable();

            await _repository.GetByIdAsync(_user.Id);
        }

        [Fact]
        public async Task CanUpdate()
        {
            var filter = Builders<User>.Filter.Eq(x => x.Id, _user.Id);
            Collection.Setup(c => c.ReplaceOneAsync(filter, _user,
                    (ReplaceOptions)null, CancellationToken.None))
                .Returns(Task.CompletedTask as Task<ReplaceOneResult>)
                .Verifiable();

            await _repository.UpdateAsync(_user.Id, _user);
        }
    }
}