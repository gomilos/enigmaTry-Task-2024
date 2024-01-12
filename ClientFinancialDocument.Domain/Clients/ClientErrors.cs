using ClientFinancialDocument.Domain.Shared;

namespace ClientFinancialDocument.Domain.Clients
{
    public static class ClientErrors
    {
        public static readonly Error NotFound = Error.NotFound("Client.NotFound", "The Client was not found");
        public static readonly Error NotWhitelistedPerTenant = Error.Forbiden("Client.NotWhitelistedPerTenant", "The Client was not whitelisted for Tenant");
        public static readonly Error SmallCompanyType = Error.Forbiden("Client.SmallCompanyType", "The Client's Company type must not be Small");
    }
}
