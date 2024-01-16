using ClientFinancialDocument.Domain.FinancialDocuments;
using ClientFinancialDocument.Infrastructure.Data;

namespace ClientFinancialDocument.Infrastructure.Persistance
{
    public sealed class FinancialDocumentRepository : IFinancialDocumentRepository
    {
        public async Task<string> GetDocument(Guid tenantId, Guid documentId, CancellationToken cancellationToken = default)
        {
            //FIXME: check how Fin docu structure depends on ProductCode, so we need some kind of Factory for Fin Docu
            return await Task.FromResult(MockFinancialDocuments.FDForProductA);
        }
    }
}
