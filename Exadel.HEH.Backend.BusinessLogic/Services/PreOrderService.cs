using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.BusinessLogic.Services
{
    public class PreOrderService : BaseService<PreOrder>
    {
        public PreOrderService(IRepository<PreOrder> repository)
            : base(repository)
        {
        }
    }
}