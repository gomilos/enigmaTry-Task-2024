using ClientFinancialDocument.Domain.Abstraction;

namespace ClientFinancialDocument.Application.Tenants.Query
{
    public static class TenantErrors
    {
        public static Error NotFound = new(
       "Tenant.NotFound",
       "The Tenant was not found");
    }
}
