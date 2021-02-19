using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.Host.Controllers.OData;
using Moq;
using Xunit;

namespace Exadel.HEH.Backend.Host.Tests
{
    public class HistoryControllerTests : BaseControllerTests<HistoryDto>
    {
        private readonly HistoryController _controller;
        private readonly HistoryDto _history;

        public HistoryControllerTests()
        {
            var historyService = new Mock<IHistoryService>();

            _controller = new HistoryController(historyService.Object);
            _history = new HistoryDto
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

            historyService.Setup(s => s.GetAllAsync())
                .ReturnsAsync(new List<HistoryDto> { _history }.AsQueryable);
        }

        [Fact]
        public async Task CanGetAll()
        {
            Data.Add(_history);
            var result = await _controller.Get();
            Assert.Single(result);
            Assert.Equal(result.First(), _history);
        }
    }
}