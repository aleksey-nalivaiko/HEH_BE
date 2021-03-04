using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Exadel.HEH.Backend.BusinessLogic.Extensions;
using Exadel.HEH.Backend.BusinessLogic.Services;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;
using Moq;
using Xunit;

namespace Exadel.HEH.Backend.BusinessLogic.Tests
{
    public class StatisticsServiceTests : BaseServiceTests<Statistics>
    {
        private readonly IStatisticsService _service;
        private Statistics _statistics;
        private IMapper _mapper;

        public StatisticsServiceTests()
        {
            var searchService = new Mock<ISearchService<Discount, Discount>>();
            var repository = new Mock<IStatisticsRepository>();
            _mapper = MapperExtensions.Mapper;
            _service = new StatisticsService(repository.Object, _mapper, searchService.Object);

            repository.Setup(r => r.StatisticsExists(It.IsAny<Expression<Func<Statistics, bool>>>()))
                .Returns(() => Task.FromResult(Data.Any(s => s.Id == _statistics.Id)));
            repository.Setup(c => c.UpdateIncrementAsync(It.IsAny<Expression<Func<Statistics, bool>>>(),
                    It.IsAny<Expression<Func<Statistics, int>>>(), It.IsAny<int>()))
                .Callback(() => Data[0].ViewsAmount += 1)
                .Returns(Task.CompletedTask);
            repository.Setup(r => r.CreateAsync(It.IsAny<Statistics>()))
                .Callback((Statistics item) =>
                {
                    Data.Add(item);
                })
                .Returns(Task.CompletedTask);
            repository.Setup(r => r.RemoveAsync(It.IsAny<Expression<Func<Statistics, bool>>>()))
                .Callback(() =>
                {
                    var item = Data.FirstOrDefault(s => s.DiscountId == _statistics.DiscountId);
                    Data.Remove(item);
                })
                .Returns(Task.CompletedTask);

            InitializeData();
        }

        [Fact]
        public async Task CanIncrementViewsAmountAsync()
        {
            Data.Add(_statistics);
            await _service.IncrementViewsAmountAsync(_statistics.DiscountId);
            Assert.Equal(101, Data[0].ViewsAmount);
            await _service.RemoveAsync(_statistics.DiscountId);
            await _service.IncrementViewsAmountAsync(Guid.NewGuid());
            Assert.Equal(1, Data[0].ViewsAmount);
        }

        [Fact]
        public async Task CanRemoveAsync()
        {
            Data.Add(_statistics);
            await _service.RemoveAsync(_statistics.DiscountId);
            Assert.Empty(Data);
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