using ClientFinancialDocument.Application.Abstractions.Mesaging;
namespace ClientFinancialDocument.Application.Tenants.Query
{
    public record GetTenantQuery(Guid TenantId) : IQuery<bool>;
}
