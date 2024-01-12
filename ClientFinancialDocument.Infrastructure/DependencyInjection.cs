
using ClientFinancialDocument.Application.Abstractions.Data;
using ClientFinancialDocument.Domain.Clients;
using ClientFinancialDocument.Domain.Products;
using ClientFinancialDocument.Domain.Tenants;
using ClientFinancialDocument.Infrastructure.Data;
using ClientFinancialDocument.Infrastructure.Persistance;
using Microsoft.Extensions.DependencyInjection;

namespace ClientFinancialDocument.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<IDataBaseStore, FakeDataStore>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ITenantRepository, TenantRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();

            return services;
        }
    }
}
