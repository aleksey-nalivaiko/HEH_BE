using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories;
using Moq;
using Xunit;

namespace Exadel.HEH.Backend.DataAccess.Tests
{
    public class SearchRepositoryTests : BaseRepositoryTests<DiscountSearch>
    {
        private readonly SearchRepository<DiscountSearch> _repository;
        private readonly DiscountSearch _discountSearch;

        public SearchRepositoryTests()
        {
            _repository = new SearchRepository<DiscountSearch>(Context.Object);

            _discountSearch = new DiscountSearch
            {
                Id = Guid.NewGuid(),
                Category = "Food",
                Tags = new List<string> { "Pizza" },
                Discount = "10%",
                Vendor = "Tempo",
                Cities = new List<string> { "Minsk" },
                Countries = new List<string> { "Belarus" },
                Streets = new List<string> { "Street" }
            };

            Context.Setup(c => c.SearchAsync<DiscountSearch>(It.IsAny<string>(), It.IsAny<string>()))
                .Returns((string path, string searchText) =>
                    Task.FromResult(Collection.Where(s =>
                        s.Discount.Contains(searchText, StringComparison.OrdinalIgnoreCase)
                        || s.Vendor.Contains(searchText, StringComparison.OrdinalIgnoreCase)
                        || s.Category.Contains(searchText, StringComparison.OrdinalIgnoreCase)
                        || s.Tags.Any(t => t.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                        || s.Countries.Any(t => t.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                        || s.Cities.Any(t => t.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                        || s.Streets.Any(t => t.Contains(searchText, StringComparison.OrdinalIgnoreCase)))));

            Context.Setup(c => c.RemoveAllAsync<DiscountSearch>())
                .Callback(Collection.Clear)
                .Returns(Task.CompletedTask);
        }

        [Fact]
        public void CanGet()
        {
            Collection.Add(_discountSearch);
            var result = _repository.Get();

            Assert.Single(result);
        }

        [Fact]
        public async Task CanSearchAsync()
        {
            Collection.Add(_discountSearch);
            var result = await _repository.SearchAsync("path", "food");

            Assert.Single(result);
        }

        [Fact]
        public async Task CanCreateManyAsync()
        {
            await _repository.CreateManyAsync(new List<DiscountSearch> { _discountSearch });

            Assert.Single(Collection);
        }

        [Fact]
        public async Task CanRemoveAllAsync()
        {
            Collection.Add(_discountSearch);

            await _repository.RemoveAllAsync();

            Assert.Empty(Collection);
        }
    }
}