using ClientFinancialDocument.Domain.Shared;
using MediatR;

namespace ClientFinancialDocument.Application.Abstractions.Mesaging
{
    public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
    {
    }
}
