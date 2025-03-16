using Networkie.Api.ExceptionHandlers.Abstractions;
using Networkie.Application.Common.Exceptions;

namespace Networkie.Api.ExceptionHandlers;

public class UnauthorizedExceptionHandler() 
    : BaseExceptionHandler<UnauthorizedExceptionHandler, UnauthorizedException>(StatusCodes.Status401Unauthorized)
{
}