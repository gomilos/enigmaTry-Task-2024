using ClientFinancialDocument.Application.Abstractions.Mesaging;
using ClientFinancialDocument.Domain.Abstraction;
using ClientFinancialDocument.Domain.Products;
namespace ClientFinancialDocument.Application.Products.Query
{
    public class GetProductHandler : IQueryHandler<GetProductQuery, ProductResponse>
    {
        private readonly IProductRepository _productRepository;

        public GetProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Result<ProductResponse>> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetProductByCodeAsync(request.ProductCode);

            if (product is null)
            {
                return Result.Failure<ProductResponse>(Error.None);
            }

            return new ProductResponse(product.ProductCode);
        }
    }
}
