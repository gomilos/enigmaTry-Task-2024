using ClientFinancialDocument.Domain.Abstraction;

namespace ClientFinancialDocument.Domain.FinancialDocuments
{
    public sealed class FinancialDocumentsService : IFinancialDocumentsService
    {
        private readonly IFinancialDocumentRepository _repository;

        public FinancialDocumentsService(IFinancialDocumentRepository repository)
        {
            _repository = repository;
        }

        public async Task<string> GetFinancialDocumentJsonString(Guid tenantId, Guid documentId, CancellationToken cancellationToken = default)
        {
            return await _repository.GetDocument(tenantId, documentId, cancellationToken);
        }
    }
}
