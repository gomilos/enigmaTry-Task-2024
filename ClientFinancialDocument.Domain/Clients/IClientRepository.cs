namespace ClientFinancialDocument.Domain.Clients
{
    public interface IClientRepository
    {
        Task<Client?> GetWhitelistedClientByTenantIdAsync(Guid clientId, Guid tenantId, CancellationToken cancellationToken = default);
        Task<Dictionary<Guid, IEnumerable<Client>>> GetWhitelistedClientsPerTenantAsync(CancellationToken cancellationToken = default);
        Task<Dictionary<Guid, IEnumerable<Client>>> GetNotWhitelistedClientsPerTenantAsync(CancellationToken cancellationToken = default);
        Task<Client?> GetClientAsync(Guid tenantId, Guid documentId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Client>> GetWhitelistedClientsByTenantIdAsync(Guid tenantId, CancellationToken cancellationToken = default);
        Task<bool> IsClientWhitelistedByTenantIdAsync(Guid tenantId, Guid clientId, CancellationToken cancellationToken = default);
        Task<Client?> GetClientByClientVATAsync(Guid clientVAT, CancellationToken cancellationToken = default);
    }
}
