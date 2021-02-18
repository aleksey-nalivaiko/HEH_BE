using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
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
        private readonly ITimezoneProvider _timezoneProvider;

        public HistoryService(IUserRepository userRepository, IHistoryRepository historyRepository,
            IMapper mapper, IUserProvider userProvider, ITimezoneProvider timezoneProvider)
        {
            _historyRepository = historyRepository;
            _userRepository = userRepository;
            _userProvider = userProvider;
            _historyRepository = historyRepository;
            _mapper = mapper;
            _timezoneProvider = timezoneProvider;
        }

        public async Task CreateAsync(UserAction action, string description)
        {
            var user = await GetCurrentUser();

            var history = new History
            {
                Action = action,
                DateTime = DateTime.Now,
                Description = description,
                UserId = user.Id,
                UserEmail = user.Email,
                UserName = user.Name,
                UserRole = user.Role
            };

            await _historyRepository.CreateAsync(history);
        }

        public IQueryable<HistoryDto> Get()
        {
            var history = _historyRepository.Get().OrderByDescending(h => h.DateTime);

            //TODO: call provider

            return history.ProjectTo<HistoryDto>(_mapper.ConfigurationProvider);
        }

        private Task<User> GetCurrentUser()
        {
            return _userRepository.GetByIdAsync(_userProvider.GetUserId());
        }
    }
}