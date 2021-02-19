using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Exadel.HEH.Backend.BusinessLogic.DTOs;
using Exadel.HEH.Backend.BusinessLogic.Providers;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Extensions;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;
using Microsoft.AspNet.OData.Query;

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
            _mapper = mapper;
            _timezoneProvider = timezoneProvider;
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
                UserRole = user.Role
            };

            await _historyRepository.CreateAsync(history);
        }

        public Task<IEnumerable<HistoryDto>> GetAllAsync(ODataQueryOptions<HistoryDto> options)
        {
            var offset = _timezoneProvider.GetDateTimeOffset();

            var historyQueryable = options.ApplyTo(_historyRepository.Get().OrderByDescending(h => h.DateTime).Select(x => new HistoryDto
            {
                Id = x.Id,
                UserId = x.UserId,
                UserEmail = x.UserEmail,
                UserName = x.UserName,
                UserRole = x.UserRole,
                DateTime = x.DateTime,
                Action = x.Action,
                Description = x.Description
            }));

            IEnumerable<HistoryDto> history = new List<HistoryDto>();

            history = _mapper.Map(historyQueryable, history);

            foreach (var historyDto in history)
            {
                historyDto.DateTime = historyDto.DateTime.ToUniversalTime().AddMinutes(offset);
            }

            return Task.FromResult(history);
        }

        private Task<User> GetCurrentUser()
        {
            return _userRepository.GetByIdAsync(_userProvider.GetUserId());
        }
    }
}