using ClientFinancialDocument.Domain.Abstraction;
namespace ClientFinancialDocument.Domain.FinancialDocuments
{
    public sealed class FinancialDocument : Entity
    {
        public Guid DocumentId { get; set; }
        //public FinancialDocument() : base() { }
        //public FinancialDocument(Guid id) : base(id) { }
    }
}
