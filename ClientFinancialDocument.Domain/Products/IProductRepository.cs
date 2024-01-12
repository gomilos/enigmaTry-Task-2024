namespace ClientFinancialDocument.Domain.Products
{
    public interface IProductRepository
    {
        Task<Product?> GetProductAsync(string productCode, CancellationToken cancellationToken = default);
        Task<IEnumerable<Product>> GetSupportedProductsAsync(CancellationToken cancellationToken = default);
        Task<bool> IsSupportedProductAsync(string productCode, CancellationToken cancellationToken = default);
        Task<IEnumerable<Product>> GetNotSupportedProductAsync(CancellationToken cancellationToken = default);
    }
}
