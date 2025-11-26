using GearCore.Monolith.Core.ProductCore.Entities;
using GearCore.Monolith.Infra.Data.Commons.Context;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace GearCore.Monolith.Tests
{
    public class WebApiApplication : WebApplicationFactory<Program>
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.UseEnvironment("Test");

            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(AppDbContext));

                if (descriptor != null)
                    services.Remove(descriptor);

                services.AddDbContext<AppDbContext>(opts =>
                {
                    opts.UseInMemoryDatabase("InMemoryDbForTesting");
                });

                var provide = services.BuildServiceProvider();

                using var scope = provide.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                db.Products.Add(new Product
                {
                    Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                    Name = "Produto Teste",
                    Description = "Descrição teste",
                    Price = 50,
                    CreatedAt = DateTime.UtcNow
                });

                db.SaveChanges();
            });
            return base.CreateHost(builder);
        }
    }
}
