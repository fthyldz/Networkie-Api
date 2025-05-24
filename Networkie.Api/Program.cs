using Carter;
using Networkie.Api.IoC;
using Networkie.Api.Middlewares;
using Networkie.Application.IoC;
using Networkie.Infrastructure.IoC;
using Networkie.Persistence.IoC;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddPersistence(builder.Configuration)
    .AddInfrastructure(builder.Configuration)
    .AddApi(builder.Configuration);

var app = builder.Build();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    //app.UseDeveloperExceptionPage();
    app.UseCors("DevelopmentCorsPolicy");
}
else
{
    app.UseCors("ProductionCorsPolicy");
}

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<IsEmailVerifiedMiddleware>();
app.UseMiddleware<IsProfileCompletedMiddleware>();

app.MapGroup("/api").RequireAuthorization().MapCarter();

await app.Services.MigrateDatabaseAsync();

app.Run();