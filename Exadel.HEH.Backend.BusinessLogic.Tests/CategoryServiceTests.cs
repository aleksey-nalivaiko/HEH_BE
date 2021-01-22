using System;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.Services;
using Exadel.HEH.Backend.DataAccess;
using Exadel.HEH.Backend.DataAccess.Models;
using Xunit;

namespace Exadel.HEH.Backend.BusinessLogic.Tests
{
    //public class CategoryServiceTests : ServiceTests<Category>
    //{
    //    private readonly Category _category;
    //    private readonly CategoryService _service;

    //    public CategoryServiceTests()
    //    {
    //        _service = new CategoryService(Repository.Object);
    //        _category = new Category
    //        {
    //            Id = Guid.NewGuid(),
    //            Name = "CategoryName"
    //        };
    //    }

    //    [Fact]
    //    public async Task CanGetAll()
    //    {
    //        Data.Add(_category);

    //        var result = await _service.Get();
    //        Assert.Single(result);
    //    }

    //    [Fact]
    //    public async Task CanGetById()
    //    {
    //        Data.Add(_category);

    //        var result = await _service.GetByIdAsync(_category.Id);
    //        Assert.Equal(_category, result);
    //    }

    //    [Fact]
    //    public async Task CanUpdate()
    //    {
    //        Data.Add(_category.DeepClone());
    //        _category.Name = "NewCategoryName";

    //        await _service.UpdateAsync(_category.Id, _category);
    //        Assert.Equal("NewCategoryName", Data.Single(x => x.Id == _category.Id).Name);
    //    }
    //}
}
