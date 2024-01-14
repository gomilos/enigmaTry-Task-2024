using ClientFinancialDocument.Domain.Clients;
using ClientFinancialDocument.Domain.Common;
using ClientFinancialDocument.Domain.Shared;

namespace ClientFinancialDocument.Domain.Abstraction
{
    public interface IHandleFinancialDocumentServise
    {
        Result<dynamic> ModifyFinancialDocument(string jsonString, int registrationMumber, CompanyType companyType, ProductCode productCode);
    }
}
