using Exadel.HEH.Backend.BusinessLogic.Services.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.BusinessLogic.Services
{
    public class TagService : Service<Tag>
    {
        public TagService(IRepository<Tag> repository)
            : base(repository)
        {
        }
    }
}
