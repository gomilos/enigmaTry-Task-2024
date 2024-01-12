using ClientFinancialDocument.Domain.Abstraction;
using System.Reflection;

namespace ClientFinancialDocument.Domain.FinancialDocuments
{
    public sealed class FinancialDocument : Entity
    {
        public Guid DocumentId { get; set; }
        public Company Company { get; set; }
        public string Data { get; set; } = string.Empty;
        //public FinancialDocument() : base() { }
        //public FinancialDocument(Guid id) : base(id) { }
    }
}
