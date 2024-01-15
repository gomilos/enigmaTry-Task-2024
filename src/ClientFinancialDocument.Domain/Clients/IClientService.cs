using ClientFinancialDocument.Domain.Shared;

namespace ClientFinancialDocument.Domain.Clients
{
    public interface IClientService
    {
        Task<Result> IsClientPerTenantWhitelisted(Guid tenantId, Guid clientId);
    }
}
