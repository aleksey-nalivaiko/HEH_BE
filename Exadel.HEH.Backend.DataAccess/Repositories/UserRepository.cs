using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Models;

namespace Exadel.HEH.Backend.DataAccess.Repositories
{
    public class UserRepository : MongoRepository<User>
    {
        public UserRepository(string connectionString)
            : base(connectionString)
        {
        }

        public Task<IEnumerable<User>> GetAllAsync()
        {
            return GetAllBaseAsync();
        }

        public Task UpdateAsync(Guid id, User user)
        {
            return UpdateBaseAsync(id, user);
        }

        public Task<User> GetByIdAsync(Guid id)
        {
            return GetByIdBaseAsync(id);
        }
    }
}