using ClientFinancialDocument.Domain.Products;
using ClientFinancialDocument.Domain.Tenants;

namespace ClientFinancialDocument.Application.Abstractions.Data
{
    public interface IDataBaseService
    {
        public Task<IEnumerable<Product>> GetAllProductsAsync();
        public Task<IEnumerable<Product>> GetAllSupportedProductsAsync();
        //public Task<Product> GetProductByProductCodeAsync(int id);

        public Task<IEnumerable<Tenant>> GetWhitelistedTenantAsync();
    }
}
