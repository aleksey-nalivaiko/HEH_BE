using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Exadel.HEH.Backend.BusinessLogic.ValidationServices.Abstract
{
    public interface ITagValidationService
    {
        Task<bool> TagExistsAsync(Guid tagId, CancellationToken token = default);

        Task<bool> TagIdNotExistsAsync(Guid tagId, CancellationToken token = default);

        Task<bool> TagsExistsAsync(IList<Guid> tags, CancellationToken token = default);

        Task<bool> TagNameNotExistsAsync(string tag, CancellationToken token = default);

        Task<bool> TagNameChangedAndNotExistsAsync(Guid tagId, string tag, CancellationToken token = default);
    }
}
