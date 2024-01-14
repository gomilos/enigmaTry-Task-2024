using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientFinancialDocument.Domain.FinancialDocuments
{
    public interface IFinancialDocumentRepository
    {
        Task<string> GetDocument(Guid tenantId, Guid documentId, CancellationToken cancellationToken = default);
    }
}
