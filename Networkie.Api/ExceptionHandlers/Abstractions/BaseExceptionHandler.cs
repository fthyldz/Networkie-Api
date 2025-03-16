using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Networkie.Application.Common.Results;

namespace Networkie.Api.ExceptionHandlers.Abstractions;

public abstract class BaseExceptionHandler<TExceptionHandler, TExceptionType>(
    int statusCode)
    : IExceptionHandler
    where TExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is not TExceptionType)
            return false;

        object? errorData = null;
        
        if (exception is ValidationException validationException)
        {
            errorData = validationException.Errors.Select(e => new
                { Message = e.ErrorMessage });
        }
            
        var error = new ErrorResult<IResult, object?>(errorData, exception.Message);

        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsJsonAsync(error, cancellationToken);

        return true;
    }
}