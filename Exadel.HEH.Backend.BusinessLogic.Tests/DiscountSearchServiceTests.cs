using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs;
using Exadel.HEH.Backend.BusinessLogic.Services;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Extensions;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;
using Moq;
using Xunit;

namespace Exadel.HEH.Backend.BusinessLogic.Tests
{
    public class DiscountSearchServiceTests : BaseServiceTests<DiscountSearch>
    {
        private readonly LuceneDiscountSearchService _searchService;
        private DiscountSearch _discountSearch;
        private Discount _discount;
        private Vendor _vendor;
        private CategoryDto _category;
        private TagDto _tag;
        private LocationDto _location;

        public DiscountSearchServiceTests()
        {
            var searchRepository = new Mock<ISearchRepository<DiscountSearch>>();
            var discountRepository = new Mock<IDiscountRepository>();
            var locationService = new Mock<ILocationService>();
            var categoryService = new Mock<ICategoryService>();
            var tagService = new Mock<ITagService>();

            _searchService = new LuceneDiscountSearchService(searchRepository.Object,
                discountRepository.Object, locationService.Object,
                categoryService.Object, tagService.Object);

            searchRepository.Setup(r => r.CreateAsync(It.IsAny<DiscountSearch>()))
                .Callback((DiscountSearch item) => { Data.Add(item); })
                .Returns(Task.CompletedTask);

            searchRepository.Setup(c => c.CreateManyAsync(It.IsAny<IEnumerable<DiscountSearch>>()))
                .Callback((IEnumerable<DiscountSearch> searches) =>
                {
                    Data.AddRange(searches);
                })
                .Returns(Task.CompletedTask);

            searchRepository.Setup(f => f.UpdateAsync(It.IsAny<DiscountSearch>()))
                .Callback((DiscountSearch item) =>
                {
                    var oldItem = Data.FirstOrDefault(x => x.Id == item.Id);
                    if (oldItem != null)
                    {
                        Data.Remove(oldItem);
                        Data.Add(item);
                    }
                })
                .Returns(Task.CompletedTask);

            searchRepository.Setup(r => r.RemoveAsync(It.IsAny<Guid>()))
                .Callback((Guid id) => { Data.RemoveAll(d => d.Id == id); })
                .Returns(Task.CompletedTask);

            searchRepository.Setup(r => r.RemoveAllAsync())
                .Callback(Data.Clear)
                .Returns(Task.CompletedTask);

            InitTestData();

            var categories = new List<CategoryDto> { _category };

            categoryService.Setup(s => s.GetByIdAsync(It.IsAny<Guid>()))
                .Returns((Guid id) => Task.FromResult(categories.First(c => c.Id == id)));

            var tags = new List<TagDto> { _tag };

            tagService.Setup(s => s.GetByIdsAsync(It.IsAny<IEnumerable<Guid>>()))
                .Returns((IEnumerable<Guid> ids) => Task.FromResult(tags.Where(c => ids.Contains(c.Id))));

            var locations = new List<LocationDto> { _location };

            locationService.Setup(s => s.GetByIdsAsync(It.IsAny<IEnumerable<Guid>>()))
                .Returns((IEnumerable<Guid> ids) => Task.FromResult(locations.Where(c => ids.Contains(c.Id))));

            var discounts = new List<Discount> { _discount };

            discountRepository.Setup(r => r.GetAllAsync())
                .Returns(() => Task.FromResult((IEnumerable<Discount>)discounts));

            discountRepository.Setup(r => r.Get())
                .Returns(() => discounts.AsQueryable());

            searchRepository.Setup(r => r.SearchAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns((string path, string searchText) =>
                    Task.FromResult(Data.Where(s => s.Discount.Contains(searchText, StringComparison.OrdinalIgnoreCase)
                                      || s.Vendor.Contains(searchText, StringComparison.OrdinalIgnoreCase)
                                      || s.Category.Contains(searchText, StringComparison.OrdinalIgnoreCase)
                                      || s.Tags.Any(t => t.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                                      || s.Countries.Any(t => t.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                                      || s.Cities.Any(t => t.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                                      || s.Streets.Any(t => t.Contains(searchText, StringComparison.OrdinalIgnoreCase)))));
        }

        [Fact]
        public async Task CanSearchAsync()
        {
            Data.Add(_discountSearch);

            var results = (await _searchService.SearchAsync("category")).ToList();
            Assert.Single(results);

            var result = results.Single();
            Assert.Equal(_discount.Id, result.Id);
        }

        [Fact]
        public async Task CanGetAllSearchAsync()
        {
            Data.Add(_discountSearch);

            var results = (await _searchService.SearchAsync(null)).ToList();
            Assert.Single(results);

            var result = results.Single();
            Assert.Equal(_discount.Id, result.Id);
        }

        [Fact]
        public async Task CanCreateAsync()
        {
            await _searchService.CreateAsync(_discount);

            Assert.Single(Data);

            var result = Data.Single();

            Assert.Equal(_discountSearch.Category, result.Category);
            Assert.Equal(_discountSearch.Discount, result.Discount);
            Assert.Equal(_discountSearch.Vendor, result.Vendor);
            Assert.Equal(_discountSearch.Tags, result.Tags);
            Assert.Equal(_discountSearch.Countries, result.Countries);
            Assert.Equal(_discountSearch.Cities, result.Cities);
            Assert.Equal(_discountSearch.Streets, result.Streets);
        }

        [Fact]
        public async Task CanUpdateAsync()
        {
            Data.Add(_discountSearch.DeepClone());

            _discount.Conditions = "10%";
            await _searchService.UpdateAsync(_discount);

            Assert.Single(Data);

            var result = Data.Single();

            Assert.Equal(_discountSearch.Category, result.Category);
            Assert.Equal(_discount.Conditions, result.Discount);
            Assert.Equal(_discountSearch.Vendor, result.Vendor);
            Assert.Equal(_discountSearch.Tags, result.Tags);
            Assert.Equal(_discountSearch.Countries, result.Countries);
            Assert.Equal(_discountSearch.Cities, result.Cities);
            Assert.Equal(_discountSearch.Streets, result.Streets);
        }

        [Fact]
        public async Task CanRemoveAsync()
        {
            Data.Add(_discountSearch);

            await _searchService.RemoveAsync(_discount.Id);

            Assert.Empty(Data);
        }

        [Fact]
        public async Task CanReindexAsync()
        {
            Data.Add(_discountSearch);

            await _searchService.ReindexAsync();

            Assert.Single(Data);

            var result = Data.Single();

            Assert.Equal(_discountSearch.Category, result.Category);
            Assert.Equal(_discountSearch.Discount, result.Discount);
            Assert.Equal(_discountSearch.Vendor, result.Vendor);
            Assert.Equal(_discountSearch.Tags, result.Tags);
            Assert.Equal(_discountSearch.Countries, result.Countries);
            Assert.Equal(_discountSearch.Cities, result.Cities);
            Assert.Equal(_discountSearch.Streets, result.Streets);
        }

        private void InitTestData()
        {
            _location = new LocationDto
            {
                Id = Guid.NewGuid(),
                Cities = new List<CityDto>
                {
                    new CityDto
                    {
                        Id = Guid.NewGuid(),
                        Name = "Minsk"
                    }
                },
                Country = "Belarus"
            };

            _vendor = new Vendor
            {
                Id = Guid.NewGuid(),
                Name = "Vendor",
                Links = new List<Link>
                {
                    new Link
                    {
                        Type = LinkType.Website,
                        Url = "v.com"
                    }
                },
                Addresses = new List<Address>
                {
                    new Address
                    {
                        Id = 1,
                        CityId = _location.Cities[0].Id,
                        CountryId = _location.Id,
                        Street = "street"
                    }
                },
                Phones = new List<Phone>
                {
                    new Phone
                    {
                        Id = 1,
                        Number = "+375441111111"
                    }
                }
            };

            _category = new CategoryDto
            {
                Id = Guid.NewGuid(),
                Name = "Category"
            };

            _tag = new TagDto
            {
                Id = Guid.NewGuid(),
                Name = "Tag",
                CategoryId = _category.Id
            };

            _discount = new Discount
            {
                Id = Guid.NewGuid(),
                PhonesIds = new List<int>
                {
                    _vendor.Phones.ElementAt(0).Id
                },
                Addresses = new List<Address>
                {
                    _vendor.Addresses.ElementAt(0)
                },
                CategoryId = _category.Id,
                Conditions = "Conditions",
                TagsIds = new List<Guid> { _tag.Id },
                VendorId = _vendor.Id,
                VendorName = _vendor.Name,
                PromoCode = "new promo code",
                StartDate = DateTime.UtcNow.Date,
                EndDate = DateTime.UtcNow.Date
            };

            _discountSearch = new DiscountSearch
            {
                Id = _discount.Id,
                Category = _category.Name,
                Tags = new List<string> { _tag.Name },
                Discount = _discount.Conditions,
                Vendor = _vendor.Name,
                Cities = new List<string> { _location.Cities[0].Name },
                Countries = new List<string> { _location.Country },
                Streets = new List<string> { _vendor.Addresses[0].Street }
            };
        }
    }
}