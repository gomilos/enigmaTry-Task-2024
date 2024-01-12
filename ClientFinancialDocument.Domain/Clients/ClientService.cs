using ClientFinancialDocument.Domain.Shared;

namespace ClientFinancialDocument.Domain.Clients
{
    public sealed class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<Result> IsClientPerTenantWhitelisted(Guid tenantId, Guid clientId)
        {
            var exist = await _clientRepository.IsClientWhitelistedByTenantIdAsync(tenantId, clientId);
            if (exist == false)
            {
                return Result.Failure(ClientErrors.NotWhitelistedPerTenant);
            }

            return Result.Success();
        }
    }
}
