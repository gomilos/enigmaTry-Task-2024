using ClientFinancialDocument.Domain.Abstraction;
namespace ClientFinancialDocument.Domain.Clients
{
    public sealed class Client : Entity
    {
        public Guid ClinetId { get; set; }
        public Guid ClientVAT { get; set; }
        public Guid TenantId { get; set; }
        public Guid DocumentId { get; set; }
        public int RegisterNumber { get; set; }
        public CompanyType CompanyType { get; set; }
        public bool Whitelisted { get; set; }
    }

    public enum CompanyType
    {
        Small,
        Medium,
        Large
    }
}
