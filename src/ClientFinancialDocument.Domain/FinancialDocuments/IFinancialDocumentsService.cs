namespace ClientFinancialDocument.Domain.FinancialDocuments
{
    public interface IFinancialDocumentsService
    {
        Task<string> GetFinancialDocumentJsonString(Guid tenantId, Guid documentId, CancellationToken cancellationToken = default);
    }
}
