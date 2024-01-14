using ClientFinancialDocument.Domain.Shared;

namespace ClientFinancialDocument.Application.Tenants.Query
{
    public static class FinancialDocumentErrors
    {
        public static readonly Error NotFound = Error.NotFound("FinancialDocument.NotFound", "The FinancialDocument was not found");
        public static readonly Error NotValidFormat = Error.Validation("FinancialDocument.NotWhitelistedTenant", "The FinancialDocument was not in valid format");
    }
}
