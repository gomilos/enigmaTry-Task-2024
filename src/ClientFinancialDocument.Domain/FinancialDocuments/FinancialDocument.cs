using ClientFinancialDocument.Domain.Abstraction;
using ClientFinancialDocument.Domain.Common;

namespace ClientFinancialDocument.Domain.FinancialDocuments
{
    public sealed class FinancialDocument : Entity
    {
        public Guid TenantId { get; set; }
        public Guid DocumentId { get; set; }
        public ProductCode ProductCode { get; set; }
    }
}
