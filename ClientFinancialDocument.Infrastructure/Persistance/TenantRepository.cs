using ClientFinancialDocument.Application.Abstractions.Data;
using ClientFinancialDocument.Domain.Tenants;

namespace ClientFinancialDocument.Infrastructure.Persistance
{
    public class TenantRepository : ITenantRepository
    {
        private readonly IDataBaseService _dbService;

        public TenantRepository(IDataBaseService dbService)
        {
            _dbService = dbService;
        }

        public async Task<Tenant?> GetTenantAsync(Guid tenantId)
        {
            var results = await _dbService.GetWhitelistedTenantAsync();
            return results.SingleOrDefault(p => p.TenantId.Equals(tenantId));
        }

        public async Task<bool> IsSupportedTenantAsync(Guid tenantId)
        {
            var results = await _dbService.GetWhitelistedTenantAsync();
            return results.Any(t => t.TenantId.Equals(tenantId));
        }

        public async Task<IEnumerable<Tenant>> GetAllWhitelistedTenantsAsync()
        {
            return await _dbService.GetWhitelistedTenantAsync();
        }
    }
}
