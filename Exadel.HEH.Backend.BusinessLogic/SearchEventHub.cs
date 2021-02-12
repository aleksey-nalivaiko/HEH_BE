using System;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;

namespace Exadel.HEH.Backend.BusinessLogic
{
    public class SearchEventHub : ISearchEventHub
    {
        public event Func<DiscountDto, Task> DiscountCreated;

        public event Func<DiscountDto, Task> DiscountUpdated;

        public event Func<Guid, Task> DiscountRemoved;

        public void SubscribeOnCreate(Func<DiscountDto, Task> subscriber)
        {
            DiscountCreated += subscriber;
        }

        public void SubscribeOnUpdate(Func<DiscountDto, Task> subscriber)
        {
            DiscountUpdated += subscriber;
        }

        public void SubscribeOnRemove(Func<Guid, Task> subscriber)
        {
            DiscountRemoved += subscriber;
        }

        public void PublishCreateEvent(DiscountDto discount)
        {
            DiscountCreated?.Invoke(discount);
        }

        public void PublishUpdateEvent(DiscountDto discount)
        {
            DiscountUpdated?.Invoke(discount);
        }

        public void PublishRemoveEvent(Guid id)
        {
            DiscountRemoved?.Invoke(id);
        }
    }
}