using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories;
using Moq;
using Xunit;

namespace Exadel.HEH.Backend.DataAccess.Tests
{
    public class StatisticsRepositoryTests : BaseRepositoryTests<Statistics>
    {
        private readonly StatisticsRepository _repository;
        private Statistics _statistics;

        public StatisticsRepositoryTests()
        {
            _repository = new StatisticsRepository(Context.Object);

            Context.Setup(c => c.ExistsAsync(It.IsAny<Expression<Func<Statistics, bool>>>()))
                .Returns(() => Task.FromResult(Collection.Any(s => s.Id == _statistics.Id)));
            Context.Setup(c => c.UpdateIncrementAsync(It.IsAny<Expression<Func<Statistics, bool>>>(),
                    It.IsAny<Expression<Func<Statistics, int>>>(), It.IsAny<int>()))
                .Callback(() => Collection[0].ViewsAmount += 1)
                .Returns(Task.CompletedTask);
            Context.Setup(c => c.GetInAndWhereAsync(It.IsAny<Expression<Func<Statistics, Guid>>>(),
                    It.IsAny<IEnumerable<Guid>>(), It.IsAny<Expression<Func<Statistics, bool>>>()))
                .Returns(() => Task.FromResult(Collection.Where(s => s.DateTime >= DateTime.Today && s.DateTime <= DateTime.Today)));

            InitializeData();
        }

        [Fact]
        public async Task CanCheckStatisticsExists()
        {
            Collection.Add(_statistics);
            var result = await _repository.StatisticsExists(x => x.Id == _statistics.Id);
            Assert.True(result);
        }

        [Fact]
        public async Task CanUpdateIncrementAsync()
        {
            Collection.Add(_statistics);
            await _repository.UpdateIncrementAsync(x => x.Id == _statistics.Id, s => s.ViewsAmount, 1);
            Assert.Equal(101, Collection[0].ViewsAmount);
        }

        [Fact]
        public async Task CanGetInWhereAsync()
        {
            Collection.Add(_statistics);
            var discountIds = new List<Guid> { _statistics.Id };
            var result =
                await _repository.GetInWhereAsync(s => s.DiscountId, discountIds, DateTime.Today, DateTime.Today);
            Assert.NotEmpty(result);
            var resultEmpty =
                await _repository.GetInWhereAsync(s => s.DiscountId, discountIds, default(DateTime), default(DateTime));
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task CanRemoveAsync()
        {
            Collection.Add(_statistics);
            await _repository.RemoveAsync(s => s.Id == _statistics.Id);
            Assert.Empty(Collection);
        }

        private void InitializeData()
        {
            _statistics = new Statistics
            {
                Id = Guid.NewGuid(),
                DiscountId = Guid.NewGuid(),
                DateTime = DateTime.Today,
                ViewsAmount = 100
            };
        }
    }
}