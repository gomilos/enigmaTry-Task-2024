using ClientFinancialDocument.Application.Abstractions.Mesaging;
namespace ClientFinancialDocument.Application.Tenants.Query
{
    public record GetTenantWhitelistedQuery(Guid TenantId) : IQuery<bool>;
}
