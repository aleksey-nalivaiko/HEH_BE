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
            IEnumerable<Guid> subscriptions,
            Expression<Func<User, bool>> expression = default);

        Task<IEnumerable<User>> GetWithSubscriptionAsync(
            Expression<Func<User, IEnumerable<Guid>>> inField,
            Guid subscription,
            Expression<Func<User, bool>> expression = default);

        Task UpdateManyAsync(IEnumerable<User> users);
    }
}