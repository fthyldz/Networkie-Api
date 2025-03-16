using Carter;
using Networkie.Api.IoC;
using Networkie.Application.IoC;
using Networkie.Infrastructure.IoC;
using Networkie.Persistence.IoC;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddPersistence()
    .AddInfrastructure()
    .AddApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseCors("DevelopmentCorsPolicy");
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseCors("ProductionCorsPolicy");
}

app.UseExceptionHandler();

app.MapGroup("/api").MapCarter();

app.Run();