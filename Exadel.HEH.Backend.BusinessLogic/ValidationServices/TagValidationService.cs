using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.ValidationServices.Abstract;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.BusinessLogic.ValidationServices
{
    public class TagValidationService : ITagValidationService
    {
        private readonly ITagRepository _tagRepository;

        public TagValidationService(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public async Task<bool> TagExistsAsync(Guid tagId, CancellationToken token = default)
        {
            return await _tagRepository.GetByIdAsync(tagId) != null;
        }

        public async Task<bool> TagNotExistsAsync(Guid tagId, CancellationToken token = default)
        {
            return await _tagRepository.GetByIdAsync(tagId) is null;
        }

        public async Task<bool> TagsExistsAsync(IList<Guid> tags, CancellationToken token = default)
        {
            var existsTags = await _tagRepository.GetAllAsync();

            var existsTagsIds = existsTags.Select(item => item.Id).ToList();

            return existsTagsIds.All(tags.Contains);
        }
    }
}