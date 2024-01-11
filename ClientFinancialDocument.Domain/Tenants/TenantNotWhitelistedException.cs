namespace ClientFinancialDocument.Domain.Products
{
    public sealed class TenantNotWhitelistedException : Exception
    {
        public TenantNotWhitelistedException(string guid) : base($"The tenant with Guid = {guid} was not found")
        { }
    }
}
