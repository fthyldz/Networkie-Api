using Networkie.Api.ExceptionHandlers.Abstractions;

namespace Networkie.Api.ExceptionHandlers;

public class GlobalExceptionHandler() 
    : BaseExceptionHandler<GlobalExceptionHandler, Exception>(StatusCodes.Status500InternalServerError)
{
}