namespace ClientFinancialDocument.Domain.Tenants
{
    public interface ITenantRepository
    {
        public Task<Tenant?> GetTenantAsync(Guid tenantId);
        Task<bool> IsSupportedTenantAsync(Guid tenantId);
        Task<IEnumerable<Tenant>> GetAllWhitelistedTenantsAsync();
    }
}
