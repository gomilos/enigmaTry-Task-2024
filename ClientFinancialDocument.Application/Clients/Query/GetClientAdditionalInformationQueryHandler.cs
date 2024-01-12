using ClientFinancialDocument.Application.Abstractions.Mesaging;
using ClientFinancialDocument.Domain.Clients;
using ClientFinancialDocument.Domain.Shared;

namespace ClientFinancialDocument.Application.Clients.Query
{
    public class GetClientAdditionalInformationQueryHandler : IQueryHandler<GetClientAdditionalInformationQuery, ClientAdditionalInformationResponse>
    {
        private readonly IClientRepository _clientRepository;
        public GetClientAdditionalInformationQueryHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }
        public async Task<Result<ClientAdditionalInformationResponse>> Handle(GetClientAdditionalInformationQuery request, CancellationToken cancellationToken)
        {
            var client = await _clientRepository.GetClientByClientVATAsync(request.ClientVAT);
            if (client == null)
            {
                return Result.Failure<ClientAdditionalInformationResponse>(ClientErrors.NotFound);
            }

            if (client.CompanyType == CompanyType.Small)
            {
                return Result.Failure<ClientAdditionalInformationResponse>(ClientErrors.SmallCompanyType);
            }

            return new ClientAdditionalInformationResponse(client.RegisterNumber, client.CompanyType);
        }
    }
}
