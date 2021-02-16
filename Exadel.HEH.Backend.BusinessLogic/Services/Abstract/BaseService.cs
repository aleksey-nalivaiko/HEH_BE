using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.BusinessLogic.Services.Abstract
{
    public abstract class BaseService<T, TDto> : IService<TDto>
        where T : class, IDataModel, new()
        where TDto : class, new()
    {
        protected readonly IRepository<T> UserRepository;
        protected readonly IMapper Mapper;

        protected BaseService(IRepository<T> userRepository, IMapper mapper)
        {
            UserRepository = userRepository;
            Mapper = mapper;
        }

        public virtual async Task<IEnumerable<TDto>> GetAllAsync()
        {
            var result = await UserRepository.GetAllAsync();
            return Mapper.Map<IEnumerable<TDto>>(result);
        }
    }
}