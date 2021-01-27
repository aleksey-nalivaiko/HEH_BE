using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Exadel.HEH.Backend.BusinessLogic.Extensions;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;
using Moq;

namespace Exadel.HEH.Backend.BusinessLogic.Tests
{
    public abstract class BaseServiceTests<T>
        where T : class, IDataModel, new()
    {
        protected readonly Mock<IRepository<T>> Repository;
        protected readonly IMapper Mapper;
        protected readonly List<T> Data;

        public BaseServiceTests()
        {
            Repository = new Mock<IRepository<T>>();
            Data = new List<T>();
            Mapper = MapperExtensions.Mapper;

            Repository.Setup(r => r.GetAllAsync())
                .Returns(() => Task.FromResult((IEnumerable<T>)Data));

            Repository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .Returns((Guid id) => Task.FromResult(Data.FirstOrDefault(x => x.Id == id)));

            Repository.Setup(r => r.CreateAsync(It.IsAny<T>()))
                .Callback((T item) => { Data.Add(item); })
                .Returns(Task.CompletedTask);

            Repository.Setup(f => f.UpdateAsync(It.IsAny<T>()))
                .Callback((T item) =>
            {
                var oldItem = Data.FirstOrDefault(x => x.Id == item.Id);
                if (oldItem != null)
                {
                    Data.Remove(oldItem);
                    Data.Add(item);
                }
            })
            .Returns(Task.CompletedTask);
        }
    }
}