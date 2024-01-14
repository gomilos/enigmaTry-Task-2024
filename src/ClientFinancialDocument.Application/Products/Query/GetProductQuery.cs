using ClientFinancialDocument.Application.Abstractions.Mesaging;

namespace ClientFinancialDocument.Application.Products.Query
{
    public record GetProductQuery(string ProductCode) : IQuery<ProductResponse>;
}
