using ClientFinancialDocument.Domain.Clients;
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

                // FIXME: didn't find valid solution to handle ErrorCode in GLobal Exception Handler for API so we return 403 and not 400 (as it is asked in Task Documentation),
                // by using MediatR validator with FluentValidation and set in it ErrorCode and use Fluent ValidationBehavior,
                // where by that ErrorCode set Response Status to be returned

                //configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });

            // FIXME: didn't find valid solution to handle ErrorCode in GLobal Exception Handler for API so we return 403 and not 400 (as it is asked in Task Documentation),
            // by using MediatR validator with FluentValidation and set in it ErrorCode and use Fluent ValidationBehavior,
            // where by that ErrorCode set Response Status to be returned

            //services.AddValidatorsFromAssembly(assembly);
            services.AddScoped<IClientService, ClientService>();

            return services;
        }
    }
}
