using ClientFinancialDocument.Application.Abstractions.Mesaging;
using ClientFinancialDocument.Domain.Shared;
using ClientFinancialDocument.Domain.Tenants;

namespace ClientFinancialDocument.Application.Tenants.Query
{
    public class GetTenantQueryHandler : IQueryHandler<GetTenantQuery, bool>
    {
        private readonly ITenantRepository _tenantRepository;
        public GetTenantQueryHandler(ITenantRepository tenantRepository)
        {
            _tenantRepository = tenantRepository;
        }
        public async Task<Result<bool>> Handle(GetTenantQuery request, CancellationToken cancellationToken)
        {
            //var tenant = await _tenantRepository.GetTenantAsync(request.TenantId, cancellationToken);
            //if (tenant == null)
            //{
            //    return Result.Failure<TenantResponse>(TenantErrors.NotFound);
            //}

            var isWhiteListedTenant = await _tenantRepository.IsWhitelistedTenantAsync(request.TenantId, cancellationToken);
            if (!isWhiteListedTenant)
            {
                return Result.Failure<bool>(TenantErrors.NotWhitelistedTenant);
            }


            return isWhiteListedTenant;
        }
    }
}
