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

        public async Task<bool> TagIdNotExistsAsync(Guid tagId, CancellationToken token = default)
        {
            return await _tagRepository.GetByIdAsync(tagId) is null;
        }

        public async Task<bool> TagsExistsAsync(IList<Guid> tags, CancellationToken token = default)
        {
            if (tags != null && tags.Any())
            {
                var allTags = (await _tagRepository.GetAllAsync())
                    .Select(t => t.Id);

                return tags.All(allTags.Contains);
            }

            return true;
        }

        public async Task<bool> TagNameNotExistsAsync(string tag, CancellationToken token = default)
        {
            var tags = await _tagRepository.GetAsync(t => t.Name == tag);

            return !tags.Any();
        }
    }
}