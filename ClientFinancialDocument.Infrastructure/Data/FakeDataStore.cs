using ClientFinancialDocument.Application.Abstractions.Data;
using ClientFinancialDocument.Domain.Common;
using ClientFinancialDocument.Domain.FinancialDocuments;
using ClientFinancialDocument.Domain.Products;
using ClientFinancialDocument.Domain.Tenants;

namespace ClientFinancialDocument.Infrastructure.Data
{
    public class FakeDataStore : IDataBaseService
    {
        private static readonly List<Product> _products = new();
        private static readonly List<Product> _supportedProducts = new();
        private static readonly List<Tenant> _supportedTenats = new();
        private static readonly List<FinancialDocument> _supportedFd = new();

        public FakeDataStore()
        {
            _supportedProducts.AddRange(new List<Product>
            {
                new Product { Id = 1, ProductCode = SupportedProductCode.ProductA.ToString() },
                new Product { Id = 2, ProductCode = SupportedProductCode.ProductB.ToString() },


            });

            _products.AddRange(new List<Product>
            {
                new Product { Id = 1, ProductCode = ProductCode.ProductA.ToString() },
                new Product { Id = 2, ProductCode = ProductCode.ProductB.ToString() },
                new Product { Id = 3, ProductCode = ProductCode.ProductC.ToString() },
                new Product { Id = 4, ProductCode = ProductCode.ProductD.ToString() }
            });

            _supportedTenats.AddRange(new List<Tenant>
            {
                new Tenant{Id = 1, TenantId = Guid.NewGuid()},
                new Tenant{Id = 2, TenantId = Guid.NewGuid()},
                new Tenant{Id = 3, TenantId = Guid.NewGuid()},
            });

            _supportedFd.AddRange(new List<FinancialDocument>
            {
                new FinancialDocument{Id = 1, DocumentId = Guid.NewGuid()},
                new FinancialDocument{Id = 2, DocumentId = Guid.NewGuid()},
                new FinancialDocument{Id = 3, DocumentId = Guid.NewGuid()},
            });
        }
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await Task.FromResult(_products);
        }

        public async Task<IEnumerable<Product>> GetAllSupportedProductsAsync()
        {
            return await Task.FromResult(_supportedProducts);
        }

        public async Task<IEnumerable<Tenant>> GetWhitelistedTenantAsync()
        {
            return await Task.FromResult(_supportedTenats);
        }

        //public async Task<Product> GetProductByProductCodeAsync(int productCode)
        //{
        //    return await Task.FromResult(_supportedProducts.Single(p => p.ProductCode.Equals(productCode)));
        //}
    }
}
