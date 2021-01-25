using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Moq;

namespace Exadel.HEH.Backend.Host.Tests
{
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

            //Service.Setup(s => s.GetByIdAsync(It.IsAny<Guid>()))
            //    .Returns((Guid id) => Task.FromResult(Data.FirstOrDefault(x => x.Id == id)));

            //Service.Setup(s => s.CreateAsync(It.IsAny<T>()))
            //    .Callback((T item) =>
            //    {
            //        Data.Add(item);
            //    })
            //    .Returns(Task.CompletedTask);

            //Service.Setup(s => s.UpdateAsync(It.IsAny<T>()))
            //    .Callback((T item) =>
            //    {
            //        var oldItem = Data.FirstOrDefault(x => x.Id == item.Id);
            //        if (oldItem != null)
            //        {
            //            Data.Remove(oldItem);
            //            Data.Add(item);
            //        }
            //    })
            //    .Returns(Task.CompletedTask);
        }
    }
}