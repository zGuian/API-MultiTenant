using GearCore.Monolith.Infra.Data.Commons.Context;
using GearCore.Monolith.WebApi.Bootstrapper;
using GearCore.Monolith.WebApi.Middlewares;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddMemoryCache();
//builder.Services.AddScoped<CacheResourceFilter>();
builder.Services.AddControllers()
    .AddJsonOptions(config =>
    {
        config.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    });

builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));
builder.Services.AddDependencyInjection(builder.Configuration);
builder.Services.AddOpenApi();
builder.Services.AddHttpContextAccessor();
builder.Services.AddRouting(options => options.LowercaseUrls = true);

var app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();

if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await context.Database.MigrateAsync();
        await SeedManualContext.SeedAsync(context);
    }

    app.MapOpenApi(pattern: "/swagger/v1/swagger.json");
    app.UseSwaggerUI(opts =>
    {
        opts.CacheLifetime = TimeSpan.FromSeconds(5);
        opts.DefaultModelsExpandDepth(-1);
        opts.DocumentTitle = "ERP";
    });
}

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<TenantMiddleware>();
app.MapControllers();
app.Run();

public partial class Program { }