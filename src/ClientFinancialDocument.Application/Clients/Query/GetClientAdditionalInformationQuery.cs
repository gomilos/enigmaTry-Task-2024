using ClientFinancialDocument.Application.Abstractions.Mesaging;

namespace ClientFinancialDocument.Application.Clients.Query
{
    public record GetClientAdditionalInformationQuery(Guid ClientVAT) : IQuery<ClientAdditionalInformationResponse>;
}
