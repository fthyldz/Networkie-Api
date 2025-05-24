using Carter;
using MediatR;
using Networkie.Api.Endpoints.DashboardModule.Models;
using Networkie.Application.Features.Dashboard.Charts.Queries.AgeBarChart;
using Networkie.Application.Features.Dashboard.Charts.Queries.CityChart;
using Networkie.Application.Features.Dashboard.Charts.Queries.CountryChart;
using Networkie.Application.Features.Dashboard.Charts.Queries.DepartmentChart;
using Networkie.Application.Features.Dashboard.Charts.Queries.EmploymentChart;
using Networkie.Application.Features.Dashboard.Charts.Queries.EmploymentStatusChart;
using Networkie.Application.Features.Dashboard.Charts.Queries.GenderPieChart;
using Networkie.Application.Features.Dashboard.Charts.Queries.ProfessionChart;
using Networkie.Application.Features.Dashboard.Charts.Queries.SocialPlatformChart;
using Networkie.Application.Features.Dashboard.Charts.Queries.UniversityChart;
using Networkie.Application.Features.Dashboard.Map.Queries.MapOverview;
using Networkie.Application.Features.Dashboard.Queries.ListData;

namespace Networkie.Api.Endpoints.DashboardModule;

public class DashboardEndpoints : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        var routeGroup = app.MapGroup("/dashboard").WithTags("Dashboard");
        
        routeGroup.MapGet("/list-data", async ([AsParameters] ListDataDto request, IMediator mediator, CancellationToken cancellationToken = default) =>
        {
            var query = new ListDataQuery(request.PageIndex, request.PageSize,
                request.FirstName,
                request.MiddleName,
                request.LastName,
                request.Gender,
                request.Profession,
                request.Country,
                request.State,
                request.City,
                request.District,
                request.University,
                request.Department,
                request.EntryYear);
            var response = await mediator.Send(query, cancellationToken);
            return Results.Ok(response);
        });
        
        var chartsRouteGroup = routeGroup.MapGroup("/charts").WithTags("Dashboard Charts");

        chartsRouteGroup.MapGet("/gender-pie-chart", async ([AsParameters] ChartDataFilterDto request, IMediator mediator, CancellationToken cancellationToken = default) =>
        {
            var query = new GenderPieChartQuery(
                request.FirstName,
                request.MiddleName,
                request.LastName,
                request.Gender,
                request.Profession,
                request.Country,
                request.State,
                request.City,
                request.District,
                request.University,
                request.Department,
                request.EntryYear);
            var response = await mediator.Send(query, cancellationToken);
            return Results.Ok(response);
        });
        
        chartsRouteGroup.MapGet("/age-bar-chart", async ([AsParameters] ChartDataFilterDto request, IMediator mediator, CancellationToken cancellationToken = default) =>
        {
            var query = new AgeBarChartQuery(
                request.FirstName,
                request.MiddleName,
                request.LastName,
                request.Gender,
                request.Profession,
                request.Country,
                request.State,
                request.City,
                request.District,
                request.University,
                request.Department,
                request.EntryYear);
            var response = await mediator.Send(query, cancellationToken);
            return Results.Ok(response);
        });
        
        chartsRouteGroup.MapGet("/university-chart", async ([AsParameters] ChartDataFilterDto request, IMediator mediator, CancellationToken cancellationToken = default) =>
        {
            var query = new UniversityChartQuery(
                request.FirstName,
                request.MiddleName,
                request.LastName,
                request.Gender,
                request.Profession,
                request.Country,
                request.State,
                request.City,
                request.District,
                request.University,
                request.Department,
                request.EntryYear);
            var response = await mediator.Send(query, cancellationToken);
            return Results.Ok(response);
        });
        
        chartsRouteGroup.MapGet("/department-chart", async ([AsParameters] ChartDataFilterDto request, IMediator mediator, CancellationToken cancellationToken = default) =>
        {
            var query = new DepartmentChartQuery(
                request.FirstName,
                request.MiddleName,
                request.LastName,
                request.Gender,
                request.Profession,
                request.Country,
                request.State,
                request.City,
                request.District,
                request.University,
                request.Department,
                request.EntryYear);
            var response = await mediator.Send(query, cancellationToken);
            return Results.Ok(response);
        });
        
        chartsRouteGroup.MapGet("/profession-chart", async ([AsParameters] ChartDataFilterDto request, IMediator mediator, CancellationToken cancellationToken = default) =>
        {
            var query = new ProfessionChartQuery(
                request.FirstName,
                request.MiddleName,
                request.LastName,
                request.Gender,
                request.Profession,
                request.Country,
                request.State,
                request.City,
                request.District,
                request.University,
                request.Department,
                request.EntryYear);
            var response = await mediator.Send(query, cancellationToken);
            return Results.Ok(response);
        });
        
        chartsRouteGroup.MapGet("/country-chart", async ([AsParameters] ChartDataFilterDto request, IMediator mediator, CancellationToken cancellationToken = default) =>
        {
            var query = new CountryChartQuery(
                request.FirstName,
                request.MiddleName,
                request.LastName,
                request.Gender,
                request.Profession,
                request.Country,
                request.State,
                request.City,
                request.District,
                request.University,
                request.Department,
                request.EntryYear);
            var response = await mediator.Send(query, cancellationToken);
            return Results.Ok(response);
        });
        
        chartsRouteGroup.MapGet("/city-chart", async ([AsParameters] ChartDataFilterDto request, IMediator mediator, CancellationToken cancellationToken = default) =>
        {
            var query = new CityChartQuery(
                request.FirstName,
                request.MiddleName,
                request.LastName,
                request.Gender,
                request.Profession,
                request.Country,
                request.State,
                request.City,
                request.District,
                request.University,
                request.Department,
                request.EntryYear);
            var response = await mediator.Send(query, cancellationToken);
            return Results.Ok(response);
        });
        
        chartsRouteGroup.MapGet("/employment-chart", async ([AsParameters] ChartDataFilterDto request, IMediator mediator, CancellationToken cancellationToken = default) =>
        {
            var query = new EmploymentChartQuery(
                request.FirstName,
                request.MiddleName,
                request.LastName,
                request.Gender,
                request.Profession,
                request.Country,
                request.State,
                request.City,
                request.District,
                request.University,
                request.Department,
                request.EntryYear);
            var response = await mediator.Send(query, cancellationToken);
            return Results.Ok(response);
        });
        
        chartsRouteGroup.MapGet("/employment-status-chart", async ([AsParameters] ChartDataFilterDto request, IMediator mediator, CancellationToken cancellationToken = default) =>
        {
            var query = new EmploymentStatusChartQuery(
                request.FirstName,
                request.MiddleName,
                request.LastName,
                request.Gender,
                request.Profession,
                request.Country,
                request.State,
                request.City,
                request.District,
                request.University,
                request.Department,
                request.EntryYear);
            var response = await mediator.Send(query, cancellationToken);
            return Results.Ok(response);
        });
        
        chartsRouteGroup.MapGet("/social-platform-chart", async ([AsParameters] ChartDataFilterDto request, IMediator mediator, CancellationToken cancellationToken = default) =>
        {
            var query = new SocialPlatformChartQuery(
                request.FirstName,
                request.MiddleName,
                request.LastName,
                request.Gender,
                request.Profession,
                request.Country,
                request.State,
                request.City,
                request.District,
                request.University,
                request.Department,
                request.EntryYear);
            var response = await mediator.Send(query, cancellationToken);
            return Results.Ok(response);
        });
        
        var mapRouteGroup = routeGroup.MapGroup("/map").WithTags("Dashboard Map");

        mapRouteGroup.MapGet("/map-overview", async ([AsParameters] ChartDataFilterDto request, IMediator mediator, CancellationToken cancellationToken = default) =>
        {
            var query = new MapOverviewQuery(
                request.FirstName,
                request.MiddleName,
                request.LastName,
                request.Gender,
                request.Profession,
                request.Country,
                request.State,
                request.City,
                request.District,
                request.University,
                request.Department,
                request.EntryYear);
            var response = await mediator.Send(query, cancellationToken);
            return Results.Ok(response);
        });
    }
}