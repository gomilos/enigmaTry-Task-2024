using ClientFinancialDocument.Domain.Abstraction;

namespace ClientFinancialDocument.Domain.Tenants
{
    public class Tenant : Entity
    {
        public Guid TenantId { get; set; }
    }
}
