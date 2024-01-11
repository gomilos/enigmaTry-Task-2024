using ClientFinancialDocument.Domain.Abstraction;
using MediatR;

namespace ClientFinancialDocument.Application.Abstractions.Mesaging
{
    public interface IQuery : IRequest, IQueryBase
    {
    }

    public interface IQuery<TResponse> : IRequest<Result<TResponse>>, IQueryBase
    {
    }

    public interface IQueryBase
    {
    }
}
