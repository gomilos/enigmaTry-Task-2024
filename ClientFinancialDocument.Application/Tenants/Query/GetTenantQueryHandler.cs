﻿using ClientFinancialDocument.Application.Abstractions.Mesaging;
using ClientFinancialDocument.Domain.Abstraction;
using ClientFinancialDocument.Domain.Tenants;

namespace ClientFinancialDocument.Application.Tenants.Query
{
    public class GetTenantQueryHandler : IQueryHandler<GetTenantQuery, TenantResponse>
    {
        private readonly ITenantRepository _tenantRepository;
        public GetTenantQueryHandler(ITenantRepository tenantRepository)
        {
            _tenantRepository = tenantRepository;
        }
        public async Task<Result<TenantResponse>> Handle(GetTenantQuery request, CancellationToken cancellationToken)
        {
            var tenant = await _tenantRepository.GetTenantAsync(request.TenantId, cancellationToken);
            if (tenant == null)
            {
                return Result.Failure<TenantResponse>(TenantErrors.NotFound);
            }


            return new TenantResponse(tenant.TenantId.ToString());
        }
    }
}