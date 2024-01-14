using ClientFinancialDocument.Domain.Abstraction;

namespace ClientFinancialDocument.Domain.FinancialDocuments
{
    public sealed class Company : Entity
    {
        public string RegistrationNumber { get; set; } = string.Empty;
        public string CompanyType { get; set; } = string.Empty;
    }
}
