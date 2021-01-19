using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Exadel.HEH.Backend.Host
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiUserController
    {
        private readonly UserRepository _userRepository;

        public ApiUserController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public Task<IEnumerable<User>> Get()
        {
            return _userRepository.GetAllAsync();
        }

        [HttpGet("{id}")]
        public Task<User> Get(Guid id)
        {
            var user = _userRepository.GetByIdAsync(id);

            return user == null ? null : _userRepository.GetByIdAsync(id);
        }

        [HttpPut("{id}")]
        public Task<User> Put(Guid id, User userInsert)
        {
            var user = _userRepository.GetByIdAsync(id);

            if (user == null)
            {
                return null;
            }

            _userRepository.UpdateAsync(id, userInsert);

            return _userRepository.GetByIdAsync(id);
        }
    }
}