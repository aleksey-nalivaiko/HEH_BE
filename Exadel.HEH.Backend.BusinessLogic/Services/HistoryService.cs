using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Create;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.BusinessLogic.Services
{
    public class HistoryService : BaseService<History, HistoryDto>, IHistoryService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserProvider _userProvider;
        private readonly IRepository<History> _historyRepository;
        private readonly IMapper _mapper;

        public HistoryService(IUserRepository userRepository, IRepository<History> historyRepository,
            IMapper mapper, IUserProvider userProvider)
            : base(historyRepository, mapper)
        {
            _userRepository = userRepository;
            _userProvider = userProvider;
            _historyRepository = historyRepository;
            _mapper = mapper;
        }

        public async Task CreateAsync(UserAction action, string description)
        {
            var user = await GetCurrentUser();

            var history = new HistoryCreateDto
            {
                Action = action,
                DateTime = DateTime.Now,
                Description = description,
                UserId = user.Id,
                UserEmail = user.Email,
                UserName = user.Name,
                UserRole = user.Role
            };

            await _historyRepository.CreateAsync(_mapper.Map<History>(history));
        }

        public override async Task<IEnumerable<HistoryDto>> GetAllAsync()
        {
            var result = (await base.GetAllAsync()).OrderByDescending(h => h.DateTime).ToList();

            foreach (var history in result)
            {
                history.DateTime = history.DateTime.ToLocalTime();
            }

            return result;
        }

        private Task<User> GetCurrentUser()
        {
            return _userRepository.GetByIdAsync(_userProvider.GetUserId());
        }
    }
}