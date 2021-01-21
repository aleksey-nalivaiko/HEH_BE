using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.BusinessLogic.Services
{
    public class VendorService : BaseService<Vendor>
    {
        public VendorService(IRepository<Vendor> repository)
            : base(repository)
        {
        }
    }
}