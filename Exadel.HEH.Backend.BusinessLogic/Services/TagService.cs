using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.BusinessLogic.Services.Abstract
{
    public class TagService : Service<Tag>, ITagService
    {
        private readonly ITagRepository tagRepository;

        public TagService(ITagRepository tagRepository)
            : base(tagRepository)
        {
            this.tagRepository = tagRepository;
        }

        public Task<IEnumerable<Tag>> GetByCategoryAsync(Guid categoryId)
        {
            return tagRepository.GetByCategoryAsync(categoryId);
        }
    }
}
