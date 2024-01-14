using ClientFinancialDocument.Application.Abstractions.Data;
using ClientFinancialDocument.Domain.Clients;
using ClientFinancialDocument.Domain.Common;
using ClientFinancialDocument.Domain.FinancialDocuments;
using ClientFinancialDocument.Domain.Products;
using ClientFinancialDocument.Domain.Tenants;

namespace ClientFinancialDocument.Infrastructure.Data
{
    public class FakeDataStore : IDataBaseStore
    {
        private static readonly List<Product> _products = new();
        private static readonly List<Product> _supportedProducts = new();
        private static readonly List<Tenant> _tenants = new();
        private static readonly List<Client> _clients = new();
        private static readonly List<FinancialDocument> _supportedFd = new();


        public FakeDataStore()
        {
            _products.AddRange(new List<Product>
            {
                new Product { Id = 1, ProductCode = ProductCode.ProductA.ToString() },
                new Product { Id = 2, ProductCode = ProductCode.ProductB.ToString() },
                new Product { Id = 3, ProductCode = ProductCode.ProductC.ToString() },
                new Product { Id = 4, ProductCode = ProductCode.ProductD.ToString() }
            });

            _supportedProducts.AddRange(new List<Product>
            {
                new Product { Id = 1, ProductCode = SupportedProductCode.ProductA.ToString() },
                new Product { Id = 2, ProductCode = SupportedProductCode.ProductB.ToString() },
            });

            _tenants.AddRange(new List<Tenant>
            {
                new Tenant{Id = 1, TenantId = Guid.NewGuid(), Whitelisted = true},
                new Tenant{Id = 2, TenantId = Guid.NewGuid(), Whitelisted = true},
                new Tenant{Id = 3, TenantId = Guid.NewGuid(), Whitelisted = true},
                new Tenant{Id = 4, TenantId = Guid.NewGuid(), Whitelisted = false}
            });

            Random rnd = new Random();
            _clients.AddRange(new List<Client>
            {
                new Client{
                    Id = 1,
                    ClinetId=Guid.NewGuid(),
                    ClientVAT = Guid.NewGuid(),
                    CompanyType= CompanyType.Small,
                    DocumentId = Guid.NewGuid(),
                    RegisterNumber = rnd.Next(1, 100),
                    Whitelisted = true,
                    TenantId = _tenants.Single(t => t.Id == 1).TenantId,
                },
                new Client{
                    Id = 2,
                    ClinetId=Guid.NewGuid(),
                    ClientVAT = Guid.NewGuid(),
                    CompanyType= CompanyType.Medium,
                    DocumentId = Guid.NewGuid(),
                    RegisterNumber = rnd.Next(1, 100),
                    Whitelisted = true,
                    TenantId = _tenants.Single(t => t.Id == 2).TenantId
                },
                new Client{Id = 3,
                    ClinetId=Guid.NewGuid(),
                    ClientVAT = Guid.NewGuid(),
                    CompanyType= CompanyType.Large,
                    DocumentId = Guid.NewGuid(),
                    RegisterNumber = rnd.Next(1, 100),
                    Whitelisted = true,
                    TenantId = _tenants.Single(t => t.Id == 3).TenantId
                },
                 new Client{Id = 4,
                    ClinetId=Guid.NewGuid(),
                    ClientVAT = Guid.NewGuid(),
                    CompanyType= CompanyType.Large,
                    DocumentId = Guid.NewGuid(),
                    RegisterNumber = rnd.Next(1, 100),
                    Whitelisted = false,
                    TenantId = _tenants.Single(t => t.Id == 3).TenantId
                },
                new Client{Id = 5,
                    ClinetId=Guid.NewGuid(),
                    ClientVAT = Guid.NewGuid(),
                    CompanyType= CompanyType.Large,
                    DocumentId = Guid.NewGuid(),
                    RegisterNumber = rnd.Next(1, 100),
                    Whitelisted = true,
                    TenantId = _tenants.Single(t => t.Id == 4).TenantId
                },
            });

            _supportedFd.AddRange(new List<FinancialDocument>
            {
                new FinancialDocument{Id = 1, DocumentId = Guid.NewGuid()},
                new FinancialDocument{Id = 2, DocumentId = Guid.NewGuid()},
                new FinancialDocument{Id = 3, DocumentId = Guid.NewGuid()},
            });
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(CancellationToken cancellationToken = default)
        {
            return await Task.FromResult(_products);
        }

        public async Task<IEnumerable<Product>> GetSupportedProductsAsync(CancellationToken cancellationToken = default)
        {
            return await Task.FromResult(_supportedProducts);
        }

        public async Task<IEnumerable<Tenant>> GetTenantsAsync(CancellationToken cancellationToken = default)
        {
            return await Task.FromResult(_tenants);
        }

        public async Task<IEnumerable<Tenant>> GetWhitelistedTenantsAsync(CancellationToken cancellationToken = default)
        {
            return await Task.FromResult(_tenants.Where(t => t.Whitelisted));
        }

        public async Task<IEnumerable<Client>> GetClientsAsync(CancellationToken cancellationToken = default)
        {
            return await Task.FromResult(_clients);
        }

        public async Task<Dictionary<Guid, IEnumerable<Client>>> GetWhitelistedClientsPerTenantAsync(CancellationToken cancellationToken = default)
        {
            var result = GetClientsPerTenant(true);
            return await Task.FromResult(result);
        }

        public async Task<Dictionary<Guid, IEnumerable<Client>>> GetNotWhitelistedClientsPerTenantAsync(CancellationToken cancellationToken = default)
        {
            var result = GetClientsPerTenant(false);
            return await Task.FromResult(result);
        }

        private static Dictionary<Guid, IEnumerable<Client>> GetClientsPerTenant(bool whiteListed)
        {
            var clients = _clients.Where(c => c.Whitelisted == whiteListed).ToList();
            Dictionary<Guid, IEnumerable<Client>> result = new();
            foreach (var tenant in _tenants)
            {
                List<Client> list = new List<Client>();
                foreach (var client in clients.Where(c => c.TenantId == tenant.TenantId))
                {
                    list.Add(client);
                }

                result.Add(tenant.TenantId, list);
            }

            return result;
        }
    }
}
