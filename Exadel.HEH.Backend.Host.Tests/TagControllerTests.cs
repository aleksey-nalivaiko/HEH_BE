using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.Host.Controllers;
using Moq;
using Xunit;

namespace Exadel.HEH.Backend.Host.Tests
{
    public class TagControllerTests
    {
        private readonly TagController _controller;
        private readonly List<TagDto> _tagData;
        private TagDto _testTag;

        public TagControllerTests()
        {
            var service = new Mock<ITagService>();
            _controller = new TagController(service.Object);

            _tagData = new List<TagDto>();

            service.Setup(s => s.CreateAsync(It.IsAny<TagDto>()))
               .Callback((TagDto item) =>
               {
                   _tagData.Add(item);
               })
               .Returns(Task.CompletedTask);

            service.Setup(s => s.UpdateAsync(It.IsAny<TagDto>()))
                .Callback((TagDto item) =>
                {
                    var oldItem = _tagData.Single();
                    if (oldItem != null)
                    {
                        _tagData.Remove(oldItem);
                        _tagData.Add(item);
                    }
                })
                .Returns(Task.CompletedTask);

            service.Setup(r => r.RemoveAsync(It.IsAny<Guid>()))
                .Callback((Guid id) => { _tagData.RemoveAt(0); })
                .Returns(Task.CompletedTask);

            InitTestData();
        }

        [Fact]
        public async Task CanCreateAsync()
        {
            await _controller.CreateAsync(_testTag);
            Assert.Single(_tagData);
        }

        [Fact]
        public async Task CanUpdateAsync()
        {
            _tagData.Add(_testTag);
            var newTag = new TagDto
            {
                Id = Guid.NewGuid(),
                Name = "CategoryName"
            };

            await _controller.UpdateAsync(newTag);
            Assert.NotEqual(_tagData.Single().Name, _testTag.Name);
        }

        [Fact]
        public async Task CanRemoveAsync()
        {
            _tagData.Add(_testTag);
            await _controller.RemoveAsync(_testTag.Id);
            Assert.Empty(_tagData);
        }

        private void InitTestData()
        {
            _testTag = new TagDto
            {
                Id = Guid.NewGuid(),
                Name = "Tag"
            };
        }
    }
}
