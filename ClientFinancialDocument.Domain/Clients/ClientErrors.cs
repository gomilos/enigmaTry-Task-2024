using ClientFinancialDocument.Domain.Abstraction;

namespace ClientFinancialDocument.Domain.Clients
{
    public static class ClientErrors
    {
        public static Error NotFound = new(
        "Client.NotFound",
        "The Client was not found");

        public static Error NotWhitelistedPerTenant = new(
        "Client.NotWhitelistedPerTenant",
        "The Client was not whitelisted");
    }
}
