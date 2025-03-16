using MediatR;

namespace Networkie.Application.Abstractions.Cqrs;

public interface IQueryHandler<in TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IQuery<TResponse>
{
}