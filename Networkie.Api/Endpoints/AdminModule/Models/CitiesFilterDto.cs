using FluentValidation;

namespace Networkie.Api.Endpoints.AdminModule.Models;

public record CitiesFilterDto(int PageIndex = 0, int PageSize = 25,
    string? SearchCity = null, string? SearchCountry = null);

public class CitiesFilterDtoValidator : AbstractValidator<CitiesFilterDto>
{
    public CitiesFilterDtoValidator()
    {
        RuleFor(x => x.PageIndex).NotEmpty().GreaterThanOrEqualTo(0);
        RuleFor(x => x.PageSize).NotEmpty().GreaterThanOrEqualTo(25).LessThanOrEqualTo(100);
    }
}