using ClientFinancialDocument.Application.Abstractions.Data;
using ClientFinancialDocument.Domain.Products;

namespace ClientFinancialDocument.Infrastructure.Persistance
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDataBaseService _dbService;
        public ProductRepository(IDataBaseService dbService)
        {
            _dbService = dbService;
        }
        public async Task<Product?> GetProductByCodeAsync(string productCode)
        {
            var results = await _dbService.GetAllProductsAsync();
            return results.FirstOrDefault(p => p.ProductCode.Equals(productCode));
        }

        public async Task<bool> IsSupportedProductAsync(string productCode)
        {
            var results = await _dbService.GetAllSupportedProductsAsync();
            return results.Any(p => p.ProductCode.Equals(productCode));
        }
    }
}
