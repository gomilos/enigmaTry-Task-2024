using ClientFinancialDocument.Application.Abstractions.Mesaging;
using ClientFinancialDocument.Domain.Shared;
using ClientFinancialDocument.Domain.Tenants;

namespace ClientFinancialDocument.Application.Tenants.Query
{
    public class GetTenantWhitelistedQueryHandler : IQueryHandler<GetTenantWhitelistedQuery, bool>
    {
        private readonly ITenantRepository _tenantRepository;
        public GetTenantWhitelistedQueryHandler(ITenantRepository tenantRepository)
        {
            _tenantRepository = tenantRepository;
        }
        public async Task<Result<bool>> Handle(GetTenantWhitelistedQuery request, CancellationToken cancellationToken)
        {
            var isWhiteListedTenant = await _tenantRepository.IsWhitelistedTenantAsync(request.TenantId, cancellationToken);
            if (!isWhiteListedTenant)
            {
                return Result.Failure<bool>(TenantErrors.NotWhitelistedTenant);
            }


            return isWhiteListedTenant;
        }
    }
}
