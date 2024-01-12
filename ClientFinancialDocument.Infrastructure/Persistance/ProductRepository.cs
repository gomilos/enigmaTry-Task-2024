using ClientFinancialDocument.Application.Abstractions.Data;
using ClientFinancialDocument.Domain.Products;

namespace ClientFinancialDocument.Infrastructure.Persistance
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDataBaseStore _dbService;
        public ProductRepository(IDataBaseStore dbService)
        {
            _dbService = dbService;
        }
        public async Task<Product?> GetProductAsync(string productCode, CancellationToken cancellationToken = default)
        {
            var results = await _dbService.GetProductsAsync(cancellationToken);
            return results.FirstOrDefault(p => p.ProductCode.Equals(productCode));
        }

        public async Task<IEnumerable<Product>> GetSupportedProductsAsync(CancellationToken cancellationToken = default)
        {
            return await _dbService.GetSupportedProductsAsync(cancellationToken);
        }

        public async Task<IEnumerable<Product>> GetNotSupportedProductAsync(CancellationToken cancellationToken = default)
        {
            var supported = await _dbService.GetSupportedProductsAsync(cancellationToken);
            var products = await _dbService.GetProductsAsync(cancellationToken);


            IList<Product> notSupported = new List<Product>();
            HashSet<Product> result = supported.ToHashSet();
            foreach (var product in products)
            {
                if (!result.Contains(product))
                {
                    notSupported.Add(product);
                }
            }

            return notSupported;
        }

        public async Task<bool> IsSupportedProductAsync(string productCode, CancellationToken cancellationToken = default)
        {
            var results = await _dbService.GetSupportedProductsAsync(cancellationToken);
            return results.Any(p => p.ProductCode.Equals(productCode));
        }
    }
}
