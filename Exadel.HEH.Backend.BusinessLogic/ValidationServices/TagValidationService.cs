using System;
using System.Threading;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.ValidationServices.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.BusinessLogic.ValidationServices
{
    public class TagValidationService : ITagValidationService
    {
        private readonly IRepository<Tag> _tagRepository;

        public TagValidationService(IRepository<Tag> tagRepository)
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
    }
}