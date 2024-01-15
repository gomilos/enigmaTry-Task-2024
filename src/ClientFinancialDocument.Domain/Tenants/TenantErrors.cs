using ClientFinancialDocument.Domain.Shared;

namespace ClientFinancialDocument.Application.Tenants.Query
{
    public static class TenantErrors
    {
        public static readonly Error NotFound = Error.NotFound("Tenant.NotFound", "The Tenant was not found");
        public static readonly Error NotWhitelistedTenant = Error.Forbiden("Tenant.NotWhitelistedTenant", "The Tenant was not whitelisted");
    }
}
