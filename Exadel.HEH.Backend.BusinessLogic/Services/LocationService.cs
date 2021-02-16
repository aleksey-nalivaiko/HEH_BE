using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.BusinessLogic.Services
{
    public class LocationService : BaseService<Location, LocationDto>, ILocationService
    {
        private readonly ILocationRepository _locationUserRepository;

        public LocationService(ILocationRepository locationUserRepository, IMapper mapper)
            : base(locationUserRepository, mapper)
        {
            _locationUserRepository = locationUserRepository;
        }

        public async Task<IEnumerable<LocationDto>> GetByIdsAsync(IEnumerable<Guid> ids)
        {
            var locations = await _locationUserRepository.GetByIdsAsync(ids);

            return Mapper.Map<IEnumerable<LocationDto>>(locations);
        }
    }
}
