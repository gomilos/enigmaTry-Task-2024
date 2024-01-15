using ClientFinancialDocument.Domain.Clients;
using ClientFinancialDocument.Domain.Common;
using ClientFinancialDocument.Domain.Shared;

namespace ClientFinancialDocument.Domain.FinancialDocuments
{
    public interface IHandleFinancialDocumentService
    {
        Result<dynamic> ModifyFinancialDocument(string jsonString, int registrationMumber, CompanyType companyType, ProductCode productCode);
    }
}
