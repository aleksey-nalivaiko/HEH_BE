using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Exadel.HEH.Backend.BusinessLogic.DTOs;
using Exadel.HEH.Backend.BusinessLogic.Providers;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.BusinessLogic.Services
{
    public class HistoryService : IHistoryService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserProvider _userProvider;
        private readonly IHistoryRepository _historyRepository;
        private readonly IMapper _mapper;

        public HistoryService(IUserRepository userRepository, IHistoryRepository historyRepository,
            IMapper mapper, IUserProvider userProvider)
        {
            _historyRepository = historyRepository;
            _userRepository = userRepository;
            _userProvider = userProvider;
            _mapper = mapper;
        }

        public async Task CreateAsync(UserAction action, string description)
        {
            var user = await GetCurrentUser();

            var history = new History
            {
                Action = action,
                DateTime = DateTime.Now.ToUniversalTime(),
                Description = description,
                UserId = user.Id,
                UserEmail = user.Email,
                UserName = user.Name,
                UserRole = user.Role,
                Address = user.Address
            };

            await _historyRepository.CreateAsync(history);
        }

        public Task<IQueryable<HistoryDto>> GetAllAsync()
        {
            var history = _historyRepository.Get().OrderByDescending(h => h.DateTime);

            return Task.FromResult(_mapper.ProjectTo<HistoryDto>(history));
        }

        private Task<User> GetCurrentUser()
        {
            return _userRepository.GetByIdAsync(_userProvider.GetUserId());
        }
    }
}