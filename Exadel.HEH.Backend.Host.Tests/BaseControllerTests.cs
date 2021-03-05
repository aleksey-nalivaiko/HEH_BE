using System.Collections.Generic;

namespace Exadel.HEH.Backend.Host.Tests
{
    public class BaseControllerTests<TDto>
        where TDto : class, new()
    {
        protected readonly List<TDto> Data;

        public BaseControllerTests()
        {
            Data = new List<TDto>();
        }
    }
}