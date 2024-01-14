namespace ClientFinancialDocument.Domain.Tenants
{
    public interface ITenantRepository
    {
        Task<bool> IsWhitelistedTenantAsync(Guid tenantId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Tenant>> GetWhitelistedTenantsAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<Tenant>> GetNotWhitelistedTenantsAsync(CancellationToken cancellationToken = default);
    }
}
