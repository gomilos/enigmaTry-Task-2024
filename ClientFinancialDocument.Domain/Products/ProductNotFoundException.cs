namespace ClientFinancialDocument.Domain.Products
{
    public sealed class ProductNotFoundException : Exception
    {
        public ProductNotFoundException(string productCode) : base($"The product with product code = {productCode} was not found")
        { }
    }
}
