using ClientFinancialDocument.Domain.Abstraction;

namespace ClientFinancialDocument.Domain.Clients
{
    public interface IClientService
    {
        Task<Result> IsClientPerTenantWhitelisted(Guid tenantId, Guid clientId);
    }
}
