using ClientFinancialDocument.Application.Abstractions.Mesaging;
using MediatR;

namespace ClientFinancialDocument.Application.Tenants.Query
{
    public record GetTenantQuery(Guid TenantId) : IQuery<TenantResponse>;
}
