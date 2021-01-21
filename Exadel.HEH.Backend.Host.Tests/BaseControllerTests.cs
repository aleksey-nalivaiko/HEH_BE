using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.Host.Mappings;
using Moq;

namespace Exadel.HEH.Backend.Host.Tests
{
    public class BaseControllerTests<T>
        where T : class, IDataModel, new()
    {
        protected readonly Mock<IService<T>> Service;
        protected readonly IMapper Mapper;
        protected readonly List<T> Data;

        public BaseControllerTests()
        {
            Service = new Mock<IService<T>>();
            Data = new List<T>();

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new UserProfile());
                mc.AddProfile(new HistoryProfile());
            });

            Mapper = mapperConfig.CreateMapper();

            Service.Setup(s => s.GetAllAsync())
                .Returns(() => Task.FromResult((IEnumerable<T>)Data));

            Service.Setup(s => s.GetByIdAsync(It.IsAny<Guid>()))
                .Returns((Guid id) => Task.FromResult(Data.FirstOrDefault(x => x.Id == id)));

            Service.Setup(s => s.CreateAsync(It.IsAny<T>()))
                .Callback((T item) =>
                {
                    Data.Add(item);
                })
                .Returns(Task.CompletedTask);

            Service.Setup(s => s.UpdateAsync(It.IsAny<T>()))
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