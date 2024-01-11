using ClientFinancialDocument.Domain.Products;
using FluentValidation;

namespace ClientFinancialDocument.Application.Products.Query
{
    public sealed class GetProductQueryValidator : AbstractValidator<GetProductQuery>
    {
        public GetProductQueryValidator(IProductRepository productRepository)
        {
            RuleFor(pq => pq.ProductCode)
           .NotEmpty()
           .WithMessage($"The {nameof(GetProductQuery.ProductCode)} can't be empty.");

            RuleFor(pq => pq.ProductCode)
            .MustAsync(async (ProductCode, _) =>
                await productRepository.IsSupportedProductAsync(ProductCode)
            )
            .WithMessage("Product is not whitelisted");
        }
    }
}
