using ClientFinancialDocument.Domain.FinancialDocuments;
using ClientFinancialDocument.Infrastructure.Data;

namespace ClientFinancialDocument.Infrastructure.Persistance
{
    public sealed class FinancialDocumentRepository : IFinancialDocumentRepository
    {
        public async Task<string> GetDocument(Guid tenantId, Guid documentId, CancellationToken cancellationToken = default)
        {
            //FIXME: check how it depends type for FD per ProductCode
            return await Task.FromResult(MockFinancialDocuments.FDForProductA);
        }
    }
}
