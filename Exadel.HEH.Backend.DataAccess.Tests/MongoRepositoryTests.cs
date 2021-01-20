using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Models;
using Moq;

namespace Exadel.HEH.Backend.DataAccess.Tests
{
    public class MongoRepositoryTests<TDocument>
        where TDocument : class, IDataModel, new()
    {
        protected readonly Mock<IDbContext> Context;
        protected readonly List<TDocument> Collection;

        public MongoRepositoryTests()
        {
            Context = new Mock<IDbContext>();

            Collection = new List<TDocument>();

            Context.Setup(f => f.GetAll<TDocument>())
                .Returns(Collection.AsQueryable());

            Context.Setup(f => f.GetByIdAsync<TDocument>(It.IsAny<Guid>()))
                .Returns((Guid id) => Task.FromResult(Collection.FirstOrDefault(x => x.Id == id)));

            Context.Setup(f => f.CreateAsync(It.IsAny<TDocument>()))
                .Callback((TDocument doc) =>
                {
                    Collection.Add(doc);
                })
                .Returns(Task.CompletedTask);

            Context.Setup(f => f.UpdateAsync(It.IsAny<Guid>(), It.IsAny<TDocument>()))
                .Callback((Guid id, TDocument doc) =>
                {
                    var oldDoc = Collection.FirstOrDefault(x => x.Id == doc.Id);
                    if (oldDoc != null)
                    {
                        Collection.Remove(oldDoc);
                        Collection.Add(doc);
                    }
                })
                .Returns(Task.CompletedTask);
        }
    }
}