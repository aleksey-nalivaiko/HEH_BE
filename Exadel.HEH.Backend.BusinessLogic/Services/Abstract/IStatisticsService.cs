using System;
using System.Threading.Tasks;

namespace Exadel.HEH.Backend.BusinessLogic.Services.Abstract
{
    public interface IStatisticsService
    {
        Task IncrementViewsAmountAsync(Guid vendorId);
    }
}