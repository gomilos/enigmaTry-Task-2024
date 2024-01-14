using ClientFinancialDocument.Domain.Shared;
using ClientFinancialDocument.Domain.Tenants;
using FluentValidation;

namespace ClientFinancialDocument.Application.Tenants.Query
{
    internal class GetTenantQueryValidator : AbstractValidator<GetTenantWhitelistedQuery>
    {
        public GetTenantQueryValidator(ITenantRepository tenantRepository)
        {
            RuleFor(tr => tr.TenantId)
           .NotEmpty()
           .WithMessage($"The {nameof(GetTenantWhitelistedQuery.TenantId)} can't be empty.");

            RuleFor(tr => tr.TenantId)
            .MustAsync(async (TenantId, _) =>
                await tenantRepository.IsWhitelistedTenantAsync(TenantId)
            )
            .WithMessage($"Tenant is not whitelisted").WithErrorCode(ErrorType.Forbiden.ToString());
        }
    }
}
