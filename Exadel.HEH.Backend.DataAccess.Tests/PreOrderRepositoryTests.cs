//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Exadel.HEH.Backend.DataAccess.Models;
//using Exadel.HEH.Backend.DataAccess.Repositories;
//using Xunit;

//namespace Exadel.HEH.Backend.DataAccess.Tests
//{
//    public class PreOrderRepositoryTests : MongoRepositoryTests<PreOrder>
//    {
//        private readonly PreOrderRepository _repository;

//        private readonly PreOrder _preOrder;

//        public PreOrderRepositoryTests()
//        {
//            _repository = new PreOrderRepository(Context.Object);
//            _preOrder = new PreOrder
//            {
//                Id = Guid.NewGuid(),
//                DiscountId = Guid.NewGuid(),
//                UserId = Guid.NewGuid(),
//                Info = "SomeInfo",
//                OrderTime = new DateTime(2015, 7, 20, 18, 30, 25)
//            };
//        }

//        [Fact]
//        public async Task CanGetAll()
//        {
//            Collection.Add(_preOrder);

//            var result = await _repository.Get();
//            Assert.Single(result);
//        }

//        [Fact]
//        public async Task CanGetById()
//        {
//            Collection.Add(_preOrder);

//            var result = await _repository.GetByIdAsync(_preOrder.Id);
//            Assert.Equal(_preOrder, result);
//        }

//        [Fact]
//        public async Task CanUpdate()
//        {
//            Collection.Add(_preOrder.DeepClone());
//            _preOrder.Info = "NewPreOrderInfo";

//            await _repository.UpdateAsync(_preOrder.Id, _preOrder);
//            Assert.Equal("NewPreOrderInfo", Collection.Single(x => x.Id == _preOrder.Id).Info);
//        }
//    }
//}
