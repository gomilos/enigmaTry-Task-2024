namespace ClientFinancialDocument.Domain.Products
{
    public interface IProductRepository
    {
        public Task<Product?> GetProductByCodeAsync(string productCode);
        Task<bool> IsSupportedProductAsync(string productCode);
    }
}
