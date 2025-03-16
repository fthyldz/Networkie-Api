using MediatR;

namespace Networkie.Application.Abstractions.Cqrs;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}