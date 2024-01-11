
using ClientFinancialDocument.Application.Abstractions.Data;
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
            services.AddSingleton<IDataBaseService, FakeDataStore>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ITenantRepository, TenantRepository>();

            return services;
        }
    }
}
