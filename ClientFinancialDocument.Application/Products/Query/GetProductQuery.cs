using ClientFinancialDocument.Application.Abstractions.Mesaging;
using ClientFinancialDocument.Domain.Common;
using ClientFinancialDocument.Domain.Products;
using MediatR;

namespace ClientFinancialDocument.Application.Products.Query
{
    public record GetProductQuery(string ProductCode) : IQuery<ProductResponse>;
}
