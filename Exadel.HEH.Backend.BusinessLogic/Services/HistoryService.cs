using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.BusinessLogic.Services
{
    public class HistoryService : BaseService<History>
    {
        public HistoryService(IRepository<History> repository)
            : base(repository)
        {
        }
    }
}