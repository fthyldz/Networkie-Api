using FluentValidation;

namespace Networkie.Api.Endpoints.AdminModule.Models;

public record CountriesFilterDto(int PageIndex = 0, int PageSize = 25,
    string? Search = null);

public class CountriesFilterDtoValidator : AbstractValidator<CountriesFilterDto>
{
    public CountriesFilterDtoValidator()
    {
        RuleFor(x => x.PageIndex).NotEmpty().GreaterThanOrEqualTo(0);
        RuleFor(x => x.PageSize).NotEmpty().GreaterThanOrEqualTo(25).LessThanOrEqualTo(100);
    }
}