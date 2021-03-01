using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Models;

namespace Exadel.HEH.Backend.DataAccess.Repositories.Abstract
{
    public interface INotificationRepository : IRepository<Notification>
    {
        IQueryable<Notification> Get();

        Task<IEnumerable<Notification>> GetAsync(Expression<Func<Notification, bool>> expression);

        Task CreateManyAsync(IEnumerable<Notification> notifications);

        Task UpdateManyAsync(IEnumerable<Notification> notifications);

        Task RemoveAsync(Expression<Func<Notification, bool>> expression);
    }
}