using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Exadel.HEH.Backend.DataAccess.Models;
using Moq;

namespace Exadel.HEH.Backend.DataAccess.Tests
{
    public class BaseRepositoryTests<TDocument>
        where TDocument : class, IDataModel, new()
    {
        protected readonly Mock<IDbContext> Context;
        protected readonly List<TDocument> Collection;

        public BaseRepositoryTests()
        {
            Context = new Mock<IDbContext>();

            Collection = new List<TDocument>();

            Context.Setup(c => c.GetAll<TDocument>())
                .Returns(Collection.AsQueryable());

            Context.Setup(c => c.GetAsync(It.IsAny<Expression<Func<TDocument, bool>>>()))
                .Returns(Task.FromResult((IEnumerable<TDocument>)Collection));

            Context.Setup(c => c.RemoveAsync<TDocument>(It.IsAny<Guid>()))
                .Callback((Guid id) =>
                {
                    Collection.RemoveAll(x => x.Id == id);
                })
                .Returns(Task.CompletedTask);

            Context.Setup(c => c.RemoveAsync(It.IsAny<Expression<Func<TDocument, bool>>>()))
                .Callback((Expression<Func<TDocument, bool>> expression) =>
                {
                    Collection.RemoveAll(x => expression.Compile()(x));
                })
                .Returns(Task.CompletedTask);

            Context.Setup(c => c.GetByIdAsync<TDocument>(It.IsAny<Guid>()))
                .Returns((Guid id) => Task.FromResult(Collection.FirstOrDefault(x => x.Id == id)));

            Context.Setup(c => c.CreateAsync(It.IsAny<TDocument>()))
                .Callback((TDocument doc) =>
                {
                    Collection.Add(doc);
                })
                .Returns(Task.CompletedTask);

            Context.Setup(c => c.UpdateAsync(It.IsAny<TDocument>()))
                .Callback((TDocument doc) =>
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