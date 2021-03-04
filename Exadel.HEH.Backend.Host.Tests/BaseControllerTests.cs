using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Moq;

namespace Exadel.HEH.Backend.Host.Tests
{
    [ExcludeFromCodeCoverage]
    public class BaseControllerTests<TDto>
        where TDto : class, new()
    {
        protected readonly Mock<IService<TDto>> Service;
        protected readonly List<TDto> Data;

        public BaseControllerTests()
        {
            Service = new Mock<IService<TDto>>();
            Data = new List<TDto>();

            Service.Setup(s => s.GetAllAsync())
                .Returns(() => Task.FromResult((IEnumerable<TDto>)Data));
        }
    }
}