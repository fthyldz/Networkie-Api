using Networkie.Application.Abstractions.Results;

namespace Networkie.Application.Common.Results;

public class ErrorResult<T, TE>(TE errors, string? message) : IResult<T>
{
    public string? Message { get; } = message;
    public TE Errors { get; } = errors;
    
    public static ErrorResult<T, TE> Create(TE errors, string? message = null) => new(errors, message);
}