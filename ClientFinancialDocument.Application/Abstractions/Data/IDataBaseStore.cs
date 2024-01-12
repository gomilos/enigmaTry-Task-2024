using ClientFinancialDocument.Domain.Clients;
using ClientFinancialDocument.Domain.Products;
using ClientFinancialDocument.Domain.Tenants;

namespace ClientFinancialDocument.Application.Abstractions.Data
{
    public interface IDataBaseStore
    {
        Task<IEnumerable<Product>> GetProductsAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<Product>> GetSupportedProductsAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<Tenant>> GetTenantsAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<Tenant>> GetWhitelistedTenantsAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<Client>> GetClientsAsync(CancellationToken cancellationToken = default);
        Task<Dictionary<Guid, IEnumerable<Client>>> GetWhitelistedClientsPerTenantAsync(CancellationToken cancellationToken = default);
        Task<Dictionary<Guid, IEnumerable<Client>>> GetNotWhitelistedClientsPerTenantAsync(CancellationToken cancellationToken = default);
    }
}
