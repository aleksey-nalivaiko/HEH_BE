using System.Linq;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.BusinessLogic.Services
{
    public class LuceneSearchService : ISearchService
    {
        private readonly ISearchRepository _searchRepository;

        public LuceneSearchService(ISearchRepository searchRepository)
        {
            _searchRepository = searchRepository;
        }

        public IQueryable<Discount> SearchDiscounts(IQueryable<Discount> allDiscounts, string searchText)
        {
            throw new System.NotImplementedException();
        }
    }
}