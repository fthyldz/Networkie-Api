namespace Networkie.Application.Abstractions.Results;

public interface IResult<T>
{
    bool Success { get; }
    string Message { get; }
}