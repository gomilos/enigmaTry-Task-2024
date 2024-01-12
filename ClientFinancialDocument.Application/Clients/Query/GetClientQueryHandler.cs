using ClientFinancialDocument.Application.Abstractions.Mesaging;
using ClientFinancialDocument.Domain.Clients;
using ClientFinancialDocument.Domain.Shared;

namespace ClientFinancialDocument.Application.Clients.Query
{
    public class GetClientQueryHandler : IQueryHandler<GetClientQuery, ClientResponse>
    {
        private readonly IClientRepository _clientRepository;
        public GetClientQueryHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }
        public async Task<Result<ClientResponse>> Handle(GetClientQuery request, CancellationToken cancellationToken)
        {
            var client = await _clientRepository.GetClientAsync(request.TenantId, request.DocumentId);
            if (client == null)
            {
                return Result.Failure<ClientResponse>(ClientErrors.NotFound);
            }


            return new ClientResponse(client.ClinetId, client.ClientVAT);
        }
    }
}
