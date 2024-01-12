using ClientFinancialDocument.Application.Abstractions.Mesaging;

namespace ClientFinancialDocument.Application.Clients.Query
{
    public record GetClientQuery(Guid TenantId, Guid DocumentId) : IQuery<ClientResponse>;
}
