using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Models;

namespace Exadel.HEH.Backend.DataAccess.Repositories.Abstract
{
    public interface IUserRepository : IRepository<User>
    {
        IQueryable<User> Get();

        Task<User> GetByEmail(string email);

        Task<IEnumerable<User>> GetWithSubscriptionsAsync(
            Expression<Func<User, IEnumerable<Guid>>> inField,
            Expression<Func<User, bool>> expression,
            IEnumerable<Guid> subscriptions);

        Task<IEnumerable<User>> GetWithSubscriptionAsync(
            Expression<Func<User, IEnumerable<Guid>>> inField,
            Expression<Func<User, bool>> expression,
            Guid subscription);
    }
}