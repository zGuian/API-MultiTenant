using GearCore.Monolith.WebApi.Bootstrapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers().AddJsonOptions(config =>
{
    config.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
});

builder.Services.AddDependencyInjection(builder.Configuration);
builder.Services.AddOpenApi();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi(pattern: "/swagger/v1/swagger.json");
    app.UseSwaggerUI(opts =>
    {
        opts.CacheLifetime = TimeSpan.FromSeconds(5);
        opts.DefaultModelsExpandDepth(-1);
        opts.DocumentTitle = "ERP";
    });
}

app.MapControllers();
app.Run();

public partial class Program { }