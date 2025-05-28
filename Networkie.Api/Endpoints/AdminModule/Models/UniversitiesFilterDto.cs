using FluentValidation;

namespace Networkie.Api.Endpoints.AdminModule.Models;

public record UniversitiesFilterDto(int PageIndex = 0, int PageSize = 25,
    string? Search = null);

public class UniversitiesFilterDtoValidator : AbstractValidator<UniversitiesFilterDto>
{
    public UniversitiesFilterDtoValidator()
    {
        RuleFor(x => x.PageIndex).NotEmpty().GreaterThanOrEqualTo(0);
        RuleFor(x => x.PageSize).NotEmpty().GreaterThanOrEqualTo(25).LessThanOrEqualTo(100);
    }
}