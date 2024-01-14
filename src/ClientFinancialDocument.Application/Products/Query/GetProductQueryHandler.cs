using ClientFinancialDocument.Application.Abstractions.Mesaging;
using ClientFinancialDocument.Domain.Products;
using ClientFinancialDocument.Domain.Shared;

namespace ClientFinancialDocument.Application.Products.Query
{
    public class GetProductQueryHandler : IQueryHandler<GetProductQuery, ProductResponse>
    {
        private readonly IProductRepository _productRepository;

        public GetProductQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Result<ProductResponse>> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetProductAsync(request.ProductCode, cancellationToken);

            if (product is null)
            {
                return Result.Failure<ProductResponse>(Error.None);
            }

            var isSupportedProduct = await _productRepository.IsSupportedProductAsync(request.ProductCode);
            if (!isSupportedProduct)
            {
                return Result.Failure<ProductResponse>(ProductErrors.NotSupportedProduct);
            }

            return new ProductResponse(product.ProductCode);
        }
    }
}
