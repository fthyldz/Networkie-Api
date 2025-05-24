namespace Networkie.Application.Abstractions.Results;

public interface IResult<T>
{
    string? Message { get; }
}