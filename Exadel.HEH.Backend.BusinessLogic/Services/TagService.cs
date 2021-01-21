using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.BusinessLogic.Services
{
    public class TagService : Service<Tag>, ITagService
    {
        private readonly ITagRepository _tagRepository;

        public TagService(ITagRepository tagRepository)
            : base(tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public Task<IEnumerable<Tag>> GetByCategoryAsync(Guid categoryId)
        {
            return _tagRepository.GetByCategoryAsync(categoryId);
        }
    }
}
