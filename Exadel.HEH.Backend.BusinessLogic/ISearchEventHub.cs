using System;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.DTOs.Get;

namespace Exadel.HEH.Backend.BusinessLogic
{
    public interface ISearchEventHub
    {
        void SubscribeOnCreate(Func<DiscountDto, Task> subscriber);

        void SubscribeOnUpdate(Func<DiscountDto, Task> subscriber);

        void SubscribeOnRemove(Func<Guid, Task> subscriber);

        void PublishCreateEvent(DiscountDto discount);

        void PublishUpdateEvent(DiscountDto discount);

        void PublishRemoveEvent(Guid id);
    }
}