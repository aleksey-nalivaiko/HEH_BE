using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Models;
using MongoDB.Driver;

namespace Exadel.HEH.Backend.DataAccess.Repositories.Abstract
{
    public interface IUserRepository : IRepository<User>
    {
        IQueryable<User> Get();

        Task<User> GetByEmail(string email);

        Task<IEnumerable<User>> GetWithSubscriptionsAsync(IEnumerable<Guid> subscriptions);
    }
}