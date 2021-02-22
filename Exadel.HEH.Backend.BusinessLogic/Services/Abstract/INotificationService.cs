using System;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs;
using Exadel.HEH.Backend.DataAccess.Models;

namespace Exadel.HEH.Backend.BusinessLogic.Services.Abstract
{
    public interface INotificationService
    {
        IQueryable<NotificationDto> Get();

        Task<int> GetNotReadCountAsync();

        Task<NotificationDto> GetByIdAsync(Guid id);

        Task CreateDiscountNotificationsAsync(Discount discount);

        Task CreateVendorNotificationsAsync(Guid vendorId);

        Task RemoveDiscountNotificationsAsync(Guid discountId);

        Task RemoveVendorNotificationsAsync(Guid vendorId);
    }
}