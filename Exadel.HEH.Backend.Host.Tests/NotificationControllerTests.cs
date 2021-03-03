using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Exadel.HEH.Backend.BusinessLogic.DTOs;
using Exadel.HEH.Backend.BusinessLogic.Extensions;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.BusinessLogic.ValidationServices.Abstract;
using Exadel.HEH.Backend.DataAccess.Extensions;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.Host.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Exadel.HEH.Backend.Host.Tests
{
    public class NotificationControllerTests : BaseControllerTests<Notification>
    {
        private readonly NotificationController _controller;
        private readonly Controllers.OData.NotificationController _odataController;
        private readonly IMapper _mapper;
        private Notification _notification;

        public NotificationControllerTests()
        {
            var validationService = new Mock<INotificationValidationService>();
            var service = new Mock<INotificationService>();
            _odataController = new Controllers.OData.NotificationController(service.Object);
            _controller = new NotificationController(service.Object, validationService.Object);
            _notification = new Notification
            {
                Id = Guid.NewGuid(),
                Title = "Test title",
                IsRead = false
            };
            _mapper = MapperExtensions.Mapper;

            service.Setup(s => s.Get())
                .Returns(() => _mapper.ProjectTo<NotificationDto>(Data.AsQueryable()));
            service.Setup(s => s.GetNotReadCountAsync())
                .Returns(() => Task.FromResult(Data.Select(n => n.IsRead == false).Count()));
            validationService.Setup(s => s.NotificationExistsAsync(It.IsAny<Guid>()))
                .Returns((Guid id) => Task.FromResult(Data.Any(n => n.Id == id)));
            service.Setup(s => s.UpdateIsReadAsync(It.IsAny<Guid>()))
                .Callback((Guid id) =>
                {
                    Data.Find(n => n.Id == id).IsRead = true;
                })
                .Returns(Task.CompletedTask);
            service.Setup(s => s.UpdateAreReadAsync())
                .Callback(() =>
                {
                    Data.Find(n => n.Id == _notification.Id).IsRead = true;
                })
                .Returns(Task.CompletedTask);
        }

        [Fact]
        public async Task CanGet()
        {
            Data.Add(_notification);
            var result = await _odataController.GetAsync().ToListAsync();
            Assert.Single(result);
        }

        [Fact]
        public async Task CanGetNotReadCountAsync()
        {
            Data.Add(_notification);
            var result = await _controller.GetNotReadCountAsync();
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task CanUpdateIsReadAsync()
        {
            Data.Add(_notification);
            var result = await _controller.UpdateIsReadAsync(_notification.Id);
            Assert.True(Data[0].IsRead);
            var statusCodeResult = result as StatusCodeResult;
            Assert.NotNull(statusCodeResult);
            Assert.Equal(200, statusCodeResult.StatusCode);
            var resultFalse = await _controller.UpdateIsReadAsync(Guid.NewGuid());
            var statusCodeResultFalse = resultFalse as StatusCodeResult;
            Assert.NotNull(statusCodeResultFalse);
            Assert.Equal(404, statusCodeResultFalse.StatusCode);
        }

        [Fact]
        public async Task CanUpdateAreReadAsync()
        {
            Data.Add(_notification);
            await _controller.UpdateAreReadAsync();
            Assert.True(Data[0].IsRead);
        }
    }
}