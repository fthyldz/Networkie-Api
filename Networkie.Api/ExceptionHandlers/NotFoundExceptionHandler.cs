using Networkie.Api.ExceptionHandlers.Abstractions;
using Networkie.Application.Common.Exceptions;

namespace Networkie.Api.ExceptionHandlers;

public class NotFoundExceptionHandler()
    : BaseExceptionHandler<NotFoundExceptionHandler, NotFoundException>(StatusCodes.Status404NotFound)
{
}