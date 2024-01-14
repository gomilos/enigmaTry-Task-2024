using ClientFinancialDocument.Domain.Shared;

namespace ClientFinancialDocument.Domain.Products
{
    public static class ProductErrors
    {
        public static readonly Error NotFound = Error.NotFound("Product.NotFound", "The Product was not found");
        public static readonly Error NotSupportedProduct = Error.Forbiden("Product.NotSupportedProduct", "The Product was not supported");
    }
}
