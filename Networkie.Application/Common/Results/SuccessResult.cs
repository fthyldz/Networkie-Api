using Networkie.Application.Abstractions.Results;

namespace Networkie.Application.Common.Results;

public class SuccessResult<T>(T? data, string? message) : IResult<T>
{
    public string? Message { get; } = message;
    public T? Data { get; } = data;
    
    public static SuccessResult<T> Create(T data, string? message = null) => new(data, message);
}