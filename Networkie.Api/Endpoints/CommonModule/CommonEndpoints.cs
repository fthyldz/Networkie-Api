using Carter;
using MediatR;
using Networkie.Api.Endpoints.CommonModule.Models;
using Networkie.Application.Features.Common.Queries.Cities;
using Networkie.Application.Features.Common.Queries.Countries;
using Networkie.Application.Features.Common.Queries.Departments;
using Networkie.Application.Features.Common.Queries.Districts;
using Networkie.Application.Features.Common.Queries.Professions;
using Networkie.Application.Features.Common.Queries.SocialPlatforms;
using Networkie.Application.Features.Common.Queries.States;
using Networkie.Application.Features.Common.Queries.Universities;

namespace Networkie.Api.Endpoints.CommonModule;

public class CommonEndpoints : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        var routeGroup = app.MapGroup("/common").WithTags("Common");
        
        routeGroup.MapGet("/universities", async (IMediator mediator, CancellationToken cancellationToken = default) =>
        {
            var response = await mediator.Send(new UniversitiesQuery(), cancellationToken);
            return Results.Ok(response);
        });
        
        routeGroup.MapGet("/departments", async (IMediator mediator, CancellationToken cancellationToken = default) =>
        {
            var response = await mediator.Send(new DepartmentsQuery(), cancellationToken);
            return Results.Ok(response);
        });
        
        routeGroup.MapGet("/professions", async (IMediator mediator, CancellationToken cancellationToken = default) =>
        {
            var response = await mediator.Send(new ProfessionsQuery(), cancellationToken);
            return Results.Ok(response);
        });
        
        routeGroup.MapGet("/countries", async (IMediator mediator, CancellationToken cancellationToken = default) =>
        {
            var response = await mediator.Send(new CountriesQuery(), cancellationToken);
            return Results.Ok(response);
        });
        
        routeGroup.MapGet("/states", async (Guid countryId, IMediator mediator, CancellationToken cancellationToken = default) =>
        {
            var query = new StatesQuery(countryId);
            var response = await mediator.Send(query, cancellationToken);
            return Results.Ok(response);
        });
        
        routeGroup.MapGet("/cities", async ([AsParameters] CitiesDto request, IMediator mediator, CancellationToken cancellationToken = default) =>
        {
            var query = new CitiesQuery(request.CountryId, request.StateId);
            var response = await mediator.Send(query, cancellationToken);
            return Results.Ok(response);
        });
        
        routeGroup.MapGet("/districts", async (Guid cityId, IMediator mediator, CancellationToken cancellationToken = default) =>
        {
            var query = new DistrictsQuery(cityId);
            var response = await mediator.Send(query, cancellationToken);
            return Results.Ok(response);
        });
        
        routeGroup.MapGet("/social-platforms", async (IMediator mediator, CancellationToken cancellationToken = default) =>
        {
            var response = await mediator.Send(new SocialPlatformsQuery(), cancellationToken);
            return Results.Ok(response);
        });
    }
}