﻿using ClientFinancialDocument.Application.Abstractions.Data;
using ClientFinancialDocument.Domain.Clients;

namespace ClientFinancialDocument.Infrastructure.Persistance
{
    public class ClientRepository : IClientRepository
    {
        private readonly IDataBaseStore _dbService;
        public ClientRepository(IDataBaseStore dbService)
        {
            _dbService = dbService;
        }

        public async Task<Client?> GetClientAsync(Guid tenantId, Guid documentId, CancellationToken cancellationToken = default)
        {
            var result = await _dbService.GetClientsAsync(cancellationToken);
            return result.FirstOrDefault(cl => cl.TenantId.Equals(tenantId) && cl.DocumentId.Equals(documentId));
        }

        public async Task<Client?> GetClientByClientVATAsync(Guid clientVAT, CancellationToken cancellationToken = default)
        {
            var result = await _dbService.GetClientsAsync(cancellationToken);
            return result.FirstOrDefault(cl => cl.ClientVAT.Equals(clientVAT));
        }

        public async Task<bool> IsClientWhitelistedByTenantIdAsync(Guid tenantId, Guid clientId, CancellationToken cancellationToken = default)
        {
            Dictionary<Guid, IEnumerable<Client>> result = await _dbService.GetWhitelistedClientsPerTenantAsync();
            return result[tenantId].Any(c => c.ClinetId.Equals(clientId));
        }
        public async Task<Dictionary<Guid, IEnumerable<Client>>> GetWhitelistedClientsPerTenantAsync(CancellationToken cancellationToken = default)
        {
            return await _dbService.GetWhitelistedClientsPerTenantAsync(cancellationToken);
        }

        public async Task<Dictionary<Guid, IEnumerable<Client>>> GetNotWhitelistedClientsPerTenantAsync(CancellationToken cancellationToken = default)
        {
            return await _dbService.GetNotWhitelistedClientsPerTenantAsync(cancellationToken);
        }
    }
}
