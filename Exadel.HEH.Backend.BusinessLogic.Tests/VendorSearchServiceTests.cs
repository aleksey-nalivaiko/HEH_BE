using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs;
using Exadel.HEH.Backend.BusinessLogic.Extensions;
using Exadel.HEH.Backend.BusinessLogic.Services;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Extensions;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;
using Moq;
using Xunit;

namespace Exadel.HEH.Backend.BusinessLogic.Tests
{
    public class VendorSearchServiceTests : BaseServiceTests<VendorSearch>
    {
        private readonly LuceneVendorSearchService _searchService;
        private VendorSearch _vendorSearch;
        private Discount _discount;
        private VendorDto _vendor;
        private CategoryDto _category;
        private TagDto _tag;
        private LocationDto _location;

        public VendorSearchServiceTests()
        {
            var searchRepository = new Mock<ISearchRepository<VendorSearch>>();
            var vendorRepository = new Mock<IVendorRepository>();
            var discountRepository = new Mock<IDiscountRepository>();
            var locationService = new Mock<ILocationService>();
            var categoryService = new Mock<ICategoryService>();
            var tagService = new Mock<ITagService>();

            _searchService = new LuceneVendorSearchService(searchRepository.Object,
                vendorRepository.Object,
                discountRepository.Object, locationService.Object,
                categoryService.Object, tagService.Object, MapperExtensions.Mapper);

            searchRepository.Setup(r => r.CreateAsync(It.IsAny<VendorSearch>()))
                .Callback((VendorSearch item) => { Data.Add(item); })
                .Returns(Task.CompletedTask);

            searchRepository.Setup(c => c.CreateManyAsync(It.IsAny<IEnumerable<VendorSearch>>()))
                .Callback((IEnumerable<VendorSearch> searches) =>
                {
                    Data.AddRange(searches);
                })
                .Returns(Task.CompletedTask);

            searchRepository.Setup(f => f.UpdateAsync(It.IsAny<VendorSearch>()))
                .Callback((VendorSearch item) =>
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

            categoryService.Setup(s => s.GetByIdsAsync(It.IsAny<IEnumerable<Guid>>()))
                .Returns((IEnumerable<Guid> ids) => Task.FromResult(categories.Where(c => ids.Contains(c.Id))));

            var tags = new List<TagDto> { _tag };

            tagService.Setup(s => s.GetByIdsAsync(It.IsAny<IEnumerable<Guid>>()))
                .Returns((IEnumerable<Guid> ids) => Task.FromResult(tags.Where(c => ids.Contains(c.Id))));

            var locations = new List<LocationDto> { _location };

            locationService.Setup(s => s.GetByIdsAsync(It.IsAny<IEnumerable<Guid>>()))
                .Returns((IEnumerable<Guid> ids) => Task.FromResult(locations.Where(c => ids.Contains(c.Id))));

            var vendors = new List<Vendor> { Mapper.Map<Vendor>(_vendor) };

            vendorRepository.Setup(r => r.GetAllAsync())
                .Returns(() => Task.FromResult((IEnumerable<Vendor>)vendors));

            var discounts = new List<Discount> { _discount };

            discountRepository.Setup(r => r.GetAllAsync())
                .Returns(() => Task.FromResult((IEnumerable<Discount>)discounts));

            discountRepository.Setup(r => r.Get())
                .Returns(() => discounts.AsQueryable());

            searchRepository.Setup(r => r.Get())
                .Returns(Data.AsQueryable);

            searchRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .Returns((Guid id) => Task.FromResult(Data.First(s => s.Id == id)));

            searchRepository.Setup(r => r.SearchAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns((string path, string searchText) =>
                    Task.FromResult(Data.Where(s => s.Discounts.Any(d => d.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                                      || s.Vendor.Contains(searchText, StringComparison.OrdinalIgnoreCase)
                                      || s.Categories.Any(c => c.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                                      || s.Tags.Any(t => t.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                                      || s.Countries.Any(t => t.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                                      || s.Cities.Any(t => t.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                                      || s.Streets.Any(t => t.Contains(searchText, StringComparison.OrdinalIgnoreCase)))));
        }

        [Fact]
        public async Task CanSearchAsync()
        {
            Data.Add(_vendorSearch);

            var results = (await _searchService.SearchAsync("category")).ToList();
            Assert.Single(results);

            var result = results.Single();
            Assert.Equal(_vendor.Id, result.Id);
        }

        [Fact]
        public async Task CanGetAllSearchAsync()
        {
            Data.Add(_vendorSearch);

            var results = (await _searchService.SearchAsync(null)).ToList();
            Assert.Single(results);

            var result = results.Single();
            Assert.Equal(_vendor.Id, result.Id);
        }

        [Fact]
        public async Task CanGetByIdAsync()
        {
            Data.Add(_vendorSearch);

            var result = await _searchService.GetByIdAsync(_vendor.Id);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task CanCreateAsync()
        {
            await _searchService.CreateAsync(_vendor);

            Assert.Single(Data);

            var result = Data.Single();

            Assert.Equal(_vendorSearch.Categories, result.Categories);
            Assert.Equal(_vendorSearch.Discounts, result.Discounts);
            Assert.Equal(_vendorSearch.Vendor, result.Vendor);
            Assert.Equal(_vendorSearch.Tags, result.Tags);
            Assert.Equal(_vendorSearch.Countries, result.Countries);
            Assert.Equal(_vendorSearch.Cities, result.Cities);
            Assert.Equal(_vendorSearch.Streets, result.Streets);
        }

        [Fact]
        public async Task CanUpdateAsync()
        {
            Data.Add(_vendorSearch.DeepClone());

            _vendor.Name = "New vendor";
            await _searchService.UpdateAsync(_vendor);

            Assert.Single(Data);

            var result = Data.Single();

            Assert.Equal(_vendorSearch.Categories, result.Categories);
            Assert.Equal(_vendorSearch.Discounts, result.Discounts);
            Assert.Equal(_vendor.Name, result.Vendor);
            Assert.Equal(_vendorSearch.Tags, result.Tags);
            Assert.Equal(_vendorSearch.Countries, result.Countries);
            Assert.Equal(_vendorSearch.Cities, result.Cities);
            Assert.Equal(_vendorSearch.Streets, result.Streets);
        }

        [Fact]
        public async Task CanRemoveAsync()
        {
            Data.Add(_vendorSearch);

            await _searchService.RemoveAsync(_vendor.Id);

            Assert.Empty(Data);
        }

        [Fact]
        public async Task CanReindexAsync()
        {
            Data.Add(_vendorSearch);

            await _searchService.ReindexAsync();

            Assert.Single(Data);

            var result = Data.Single();

            Assert.Equal(_vendorSearch.Categories, result.Categories);
            Assert.Equal(_vendorSearch.Discounts, result.Discounts);
            Assert.Equal(_vendorSearch.Vendor, result.Vendor);
            Assert.Equal(_vendorSearch.Tags, result.Tags);
            Assert.Equal(_vendorSearch.Countries, result.Countries);
            Assert.Equal(_vendorSearch.Cities, result.Cities);
            Assert.Equal(_vendorSearch.Streets, result.Streets);
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

            _vendor = new VendorDto
            {
                Id = Guid.NewGuid(),
                Name = "Vendor",
                Links = new List<LinkDto>
                {
                    new LinkDto
                    {
                        Type = LinkType.Website,
                        Url = "v.com"
                    }
                },
                Addresses = new List<AddressDto>
                {
                    new AddressDto
                    {
                        Id = 1,
                        CityId = _location.Cities[0].Id,
                        CountryId = _location.Id,
                        Street = "street"
                    }
                },
                Phones = new List<PhoneDto>
                {
                    new PhoneDto
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
                CategoryId = _category.Id,
                Conditions = "Conditions",
                TagsIds = new List<Guid> { _tag.Id },
                VendorId = _vendor.Id,
                VendorName = _vendor.Name,
                PromoCode = "new promo code",
                StartDate = DateTime.UtcNow.Date,
                EndDate = DateTime.UtcNow.Date
            };

            _vendorSearch = new VendorSearch
            {
                Id = _vendor.Id,
                Categories = new List<string> { _category.Name },
                Tags = new List<string> { _tag.Name },
                Discounts = new List<string> { _discount.Conditions },
                Vendor = _vendor.Name,
                Cities = new List<string> { _location.Cities[0].Name },
                Countries = new List<string> { _location.Country },
                Streets = new List<string> { _vendor.Addresses.ElementAt(0).Street }
            };

            _vendor.Discounts = new List<DiscountShortDto>
            {
                Mapper.Map<DiscountShortDto>(_discount)
            };
        }
    }
}