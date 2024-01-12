using ClientFinancialDocument.Application.Abstractions.Data;
using ClientFinancialDocument.Domain.Tenants;

namespace ClientFinancialDocument.Infrastructure.Persistance
{
    public class TenantRepository : ITenantRepository
    {
        private readonly IDataBaseStore _dbService;

        public TenantRepository(IDataBaseStore dbService)
        {
            _dbService = dbService;
        }

        public async Task<Tenant?> GetTenantAsync(Guid tenantId, CancellationToken cancellationToken = default)
        {
            var results = await _dbService.GetWhitelistedTenantsAsync(cancellationToken);
            return results.SingleOrDefault(p => p.TenantId.Equals(tenantId));
        }

        public async Task<bool> IsWhitelistedTenantAsync(Guid tenantId, CancellationToken cancellationToken = default)
        {
            var results = await _dbService.GetWhitelistedTenantsAsync(cancellationToken);
            return results.Any(t => t.TenantId.Equals(tenantId));
        }

        public async Task<IEnumerable<Tenant>> GetWhitelistedTenantsAsync(CancellationToken cancellationToken = default)
        {
            return await _dbService.GetWhitelistedTenantsAsync(cancellationToken);
        }

        public async Task<IEnumerable<Tenant>> GetNotWhitelistedTenantsAsync(CancellationToken cancellationToken = default)
        {
            var whitelistedTenants = await _dbService.GetWhitelistedTenantsAsync(cancellationToken);
            var allTenants = await _dbService.GetTenantsAsync(cancellationToken);

            IList<Tenant> notWhitelisted = new List<Tenant>();
            HashSet<Tenant> result = whitelistedTenants.ToHashSet();
            foreach (var tenant in allTenants)
            {
                if (!result.Contains(tenant))
                {
                    notWhitelisted.Add(tenant);
                }
            }

            return notWhitelisted;
        }
    }
}
