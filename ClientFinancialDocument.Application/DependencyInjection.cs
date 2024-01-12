using ClientFinancialDocument.Application.Abstractions.Behaviors;
using ClientFinancialDocument.Domain.Clients;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace ClientFinancialDocument.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = typeof(DependencyInjection).Assembly;

            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(assembly);
                configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });

            services.AddValidatorsFromAssembly(assembly);
            services.AddScoped<IClientService, ClientService>();

            return services;
        }
    }
}
