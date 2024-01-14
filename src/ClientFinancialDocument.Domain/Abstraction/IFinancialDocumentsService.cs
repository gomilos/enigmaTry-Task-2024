namespace ClientFinancialDocument.Domain.Abstraction
{
    public interface IFinancialDocumentsService
    {
        Task<string> GetFinancialDocumentJsonString(Guid tenantId, Guid documentId, CancellationToken cancellationToken = default);
    }
}
