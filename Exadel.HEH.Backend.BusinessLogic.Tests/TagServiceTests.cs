using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs;
using Exadel.HEH.Backend.BusinessLogic.Extensions;
using Exadel.HEH.Backend.BusinessLogic.Services;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;
using Moq;
using Xunit;

namespace Exadel.HEH.Backend.BusinessLogic.Tests
{
    public class TagServiceTests : BaseServiceTests<Tag>
    {
        private readonly TagService _service;
        private readonly List<Discount> _discountData;
        private Tag _tag;
        private Discount _discount;

        public TagServiceTests()
        {
            _discountData = new List<Discount>();

            var tagRepository = new Mock<ITagRepository>();
            var discountRepository = new Mock<IDiscountRepository>();
            var historyService = new Mock<IHistoryService>();
            var userService = new Mock<IUserService>();

            _service = new TagService(tagRepository.Object, discountRepository.Object,
                MapperExtensions.Mapper, historyService.Object, userService.Object);

            tagRepository.Setup(r => r.GetAsync(It.IsAny<Expression<Func<Tag, bool>>>()))
                .Returns((Expression<Func<Tag, bool>> expression) =>
                    Task.FromResult(Data.Where(expression.Compile())));

            tagRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .Returns((Guid id) => Task.FromResult(Data.FirstOrDefault(x => x.Id == id)));

            tagRepository.Setup(r => r.GetByIdsAsync(It.IsAny<IEnumerable<Guid>>()))
                .Returns((IEnumerable<Guid> ids) => Task.FromResult(Data.Where(x => ids.Contains(x.Id))));

            tagRepository.Setup(r => r.CreateAsync(It.IsAny<Tag>()))
                .Callback((Tag item) => { Data.Add(item); })
                .Returns(Task.CompletedTask);

            tagRepository.Setup(f => f.UpdateAsync(It.IsAny<Tag>()))
                .Callback((Tag item) =>
                {
                    var oldItem = Data.FirstOrDefault(x => x.Id == item.Id);
                    if (oldItem != null)
                    {
                        Data.Remove(oldItem);
                        Data.Add(item);
                    }
                })
                .Returns(Task.CompletedTask);

            tagRepository.Setup(r => r.RemoveAsync(It.IsAny<Guid>()))
                .Callback((Guid id) =>
                {
                    Data.RemoveAll(x => x.Id == id);
                })
                .Returns(Task.CompletedTask);

            tagRepository.Setup(r => r.RemoveAsync(It.IsAny<Expression<Func<Tag, bool>>>()))
                .Callback((Expression<Func<Tag, bool>> expression) =>
                {
                    Data.RemoveAll(t => expression.Compile()(t));
                })
                .Returns(Task.CompletedTask);

            discountRepository.Setup(r => r.GetAllAsync())
                .Returns(Task.FromResult((IEnumerable<Discount>)_discountData));

            InitTestData();
        }

        [Fact]
        public async Task CanGetByIdsAsync()
        {
            Data.Add(_tag);

            var tag = new Tag
            {
                Id = Guid.NewGuid(),
                Name = "Food"
            };

            Data.Add(tag);

            var result = await _service.GetByIdsAsync(new List<Guid> { _tag.Id, tag.Id });

            Assert.NotNull(result);
        }

        [Fact]
        public async Task CanCreateAsync()
        {
            var newTag = new TagDto
            {
                Id = Guid.NewGuid(),
                Name = "TagName",
                CategoryId = Guid.NewGuid()
            };
            await _service.CreateAsync(newTag);
            var result = Data.Single(s => s.Id == newTag.Id);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task CanUpdateAsync()
        {
            Data.Add(_tag);
            var newTag = new TagDto
            {
                Id = _tag.Id,
                CategoryId = Guid.NewGuid(),
                Name = "NewTagName"
            };

            await _service.UpdateAsync(newTag);
            Assert.Equal("NewTagName", Data.Single(x => x.Id == _tag.Id).Name);
        }

        [Fact]
        public async Task CanRemoveAsync()
        {
            Data.Add(_tag);
            _discountData.Add(_discount);
            await _service.RemoveAsync(_tag.Id);
            Assert.Empty(Data);
        }

        [Fact]
        public async Task CanRemoveByCategoryAsync()
        {
            Data.Add(_tag);
            _discountData.Add(_discount);
            await _service.RemoveByCategoryAsync(_tag.CategoryId);
            Assert.Empty(Data);
        }

        private void InitTestData()
        {
            var category = new Category
            {
                Id = Guid.NewGuid(),
                Name = "Category1"
            };

            _tag = new Tag
            {
                Id = Guid.NewGuid(),
                Name = "Tag1",
                CategoryId = category.Id
            };

            _discount = new Discount
            {
                Id = Guid.NewGuid(),
                PhonesIds = new List<int>
                {
                    2
                },
                CategoryId = category.Id,
                Conditions = "Conditions",
                TagsIds = new List<Guid> { _tag.Id },
                VendorId = Guid.NewGuid(),
                VendorName = "Vendor",
                PromoCode = "new promo code",
                StartDate = new DateTime(2021, 1, 20),
                EndDate = new DateTime(2021, 1, 25)
            };
        }
    }
}
