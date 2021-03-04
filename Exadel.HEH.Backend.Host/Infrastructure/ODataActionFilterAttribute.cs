using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Exadel.HEH.Backend.BusinessLogic.DTOs;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Exadel.HEH.Backend.Host.Infrastructure
{
    [ExcludeFromCodeCoverage]
    public class ODataActionFilterAttribute : ActionFilterAttribute
    {
        private const string FileName = "Statistics.xlsx";
        private const string FileType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);

            var statistics = (context.Result as ObjectResult)?.Value as IEnumerable<DiscountStatisticsDto>;

            if (context.HttpContext.RequestServices.GetService(typeof(IExportService)) is IExportService exportService)
            {
                var stream = exportService.GetFileAsync(statistics).Result;
                context.Result = new FileStreamResult(stream, FileType)
                {
                    FileDownloadName = FileName
                };
            }
        }
    }
}