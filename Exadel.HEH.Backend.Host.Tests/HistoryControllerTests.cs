using System;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Create;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.Host.Controllers;
using Xunit;

namespace Exadel.HEH.Backend.Host.Tests
{
    public class HistoryControllerTests : BaseControllerTests<History>
    {
        private readonly HistoryController _controller;
        private readonly History _history;
        private readonly HistoryCreateDto _historyDto;

        //public HistoryControllerTests()
        //{
        //    _controller = new HistoryController(Service.Object);
        //    _history = new History
        //    {
        //        Id = Guid.NewGuid(),
        //        UserId = Guid.NewGuid(),
        //        Action = UserAction.Add,
        //        Description = "Added",
        //        ActionDateTime = DateTime.Now,
        //        UserEmail = "email@mail.com",
        //        UserName = "Mary",
        //        UserRole = UserRole.Moderator
        //    };

        //    _historyDto = new HistoryCreateDto
        //    {
        //        UserId = Guid.NewGuid(),
        //        Action = UserAction.Add,
        //        Description = "Added",
        //        ActionDateTime = DateTime.Now,
        //        UserEmail = "email@mail.com",
        //        UserName = "Mary",
        //        UserRole = UserRole.Moderator
        //    };
        //}

        //[Fact]
        //public async Task CanGetAll()
        //{
        //    Data.Add(_history);
        //    var result = await _controller.GetAllAsync();
        //    Assert.Single(result);
        //}

        //[Fact]
        //public async Task CanCreate()
        //{
        //    await _controller.CreateAsync(_historyDto);
        //    var history = Data.FirstOrDefault();
        //    Assert.NotNull(history);
        //}
    }
}