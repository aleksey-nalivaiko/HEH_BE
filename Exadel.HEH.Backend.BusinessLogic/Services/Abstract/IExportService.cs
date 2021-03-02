using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs;

namespace Exadel.HEH.Backend.BusinessLogic.Services.Abstract
{
    public interface IExportService
    {
        Task<MemoryStream> GetFileAsync(IEnumerable<DiscountStatisticsDto> statistics);
    }
}