using ClientFinancialDocument.Domain.Abstraction;

namespace ClientFinancialDocument.Domain.Products
{
    public sealed class Product : Entity
    {
        public string ProductCode { get; set; } = string.Empty;
    }
}
