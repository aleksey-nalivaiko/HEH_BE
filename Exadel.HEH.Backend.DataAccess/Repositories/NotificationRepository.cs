using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.DataAccess.Repositories
{
    public class NotificationRepository : BaseRepository<Notification>, INotificationRepository
    {
        public NotificationRepository(IDbContext context)
            : base(context)
        {
        }

        public IQueryable<Notification> Get()
        {
            return Context.GetAll<Notification>();
        }

        public Task<IEnumerable<Notification>> GetAsync(Expression<Func<Notification, bool>> expression)
        {
            return Context.GetAsync(expression);
        }

        public Task CreateManyAsync(IEnumerable<Notification> notifications)
        {
            return Context.CreateManyAsync(notifications);
        }

        public Task RemoveAsync(Expression<Func<Notification, bool>> expression)
        {
            return Context.RemoveAsync(expression);
        }
    }
}