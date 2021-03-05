using System.Collections.Generic;
using AutoMapper;
using Exadel.HEH.Backend.BusinessLogic.Extensions;
using Exadel.HEH.Backend.DataAccess.Models;

namespace Exadel.HEH.Backend.BusinessLogic.Tests
{
    public abstract class BaseServiceTests<T>
        where T : class, IDataModel, new()
    {
        protected readonly IMapper Mapper;
        protected readonly List<T> Data;

        protected BaseServiceTests()
        {
            Data = new List<T>();
            Mapper = MapperExtensions.Mapper;
        }
    }
}