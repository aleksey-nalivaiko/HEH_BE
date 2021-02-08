using System;
using System.Threading;
using System.Threading.Tasks;
using Exadel.HEH.Backend.BusinessLogic.ValidationServices.Abstract;
using Exadel.HEH.Backend.DataAccess.Models;
using Exadel.HEH.Backend.DataAccess.Repositories.Abstract;

namespace Exadel.HEH.Backend.BusinessLogic.ValidationServices
{
    public class VendorValidationService : IVendorValidationService
    {
        private readonly IRepository<Vendor> _vendorRepository;
        private readonly IMethodProvider _methodProvider;

        public VendorValidationService(IRepository<Vendor> vendorRepository, IMethodProvider methodProvider)
        {
            _vendorRepository = vendorRepository;
            _methodProvider = methodProvider;
        }

        public async Task<bool> VendorExists(Guid vendorId, CancellationToken token)
        {
            return await _vendorRepository.GetByIdAsync(vendorId) != null;
        }

        public async Task<bool> VendorNotExists(Guid vendorId, CancellationToken token)
        {
            return await _vendorRepository.GetByIdAsync(vendorId) is null;
        }
    }
}