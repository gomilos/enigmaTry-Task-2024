using ClientFinancialDocument.Domain.Clients;

namespace ClientFinancialDocument.Application.Clients.Query
{
    public record ClientAdditionalInformationResponse(int RegistrationMumber, CompanyType CompanyType);
}
