using System;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.Host.Controllers.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Exadel.HEH.Backend.Host.Controllers
{
    public class UserController : BaseController<UserDto>
    {
        public UserController(IService<UserDto> service)
            : base(service)
        {
        }

        [HttpGet("{id:guid}")]
        public Task<UserDto> GetByIdAsync(Guid id)
        {
            return Service.GetByIdAsync(id);
        }
    }
}