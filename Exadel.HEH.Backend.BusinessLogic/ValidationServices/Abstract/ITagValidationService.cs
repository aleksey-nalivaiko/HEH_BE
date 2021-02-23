﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Exadel.HEH.Backend.BusinessLogic.ValidationServices.Abstract
{
    public interface ITagValidationService
    {
        Task<bool> TagExistsAsync(Guid tagId, CancellationToken token = default);

        Task<bool> TagNotExistsAsync(Guid tagId, CancellationToken token = default);

        Task<bool> TagsExistsAsync(IList<Guid> tags, CancellationToken token = default);
    }
}
