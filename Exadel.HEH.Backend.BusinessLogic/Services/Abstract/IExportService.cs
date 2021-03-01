using System;
using System.IO;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs;
using Microsoft.AspNet.OData.Query;

namespace Exadel.HEH.Backend.BusinessLogic.Services.Abstract
{
    public interface IExportService
    {
        Task<MemoryStream> GetFileAsync(ODataQueryOptions<DiscountStatisticsDto> options,
            string searchText = default, DateTime startDate = default, DateTime endDate = default);
    }
}