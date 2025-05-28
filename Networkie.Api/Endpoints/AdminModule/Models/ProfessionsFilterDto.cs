using FluentValidation;

namespace Networkie.Api.Endpoints.AdminModule.Models;

public record ProfessionsFilterDto(int PageIndex = 0, int PageSize = 25,
    string? Search = null);

public class ProfessionsFilterDtoValidator : AbstractValidator<ProfessionsFilterDto>
{
    public ProfessionsFilterDtoValidator()
    {
        RuleFor(x => x.PageIndex).NotEmpty().GreaterThanOrEqualTo(0);
        RuleFor(x => x.PageSize).NotEmpty().GreaterThanOrEqualTo(25).LessThanOrEqualTo(100);
    }
}