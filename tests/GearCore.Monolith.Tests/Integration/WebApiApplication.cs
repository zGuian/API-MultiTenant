namespace GearCore.Monolith.Tests.Integration
{
    //public class WebApiApplication : WebApplicationFactory<Program>
    //{
    //    protected override IHost CreateHost(IHostBuilder builder)
    //    {
    //        builder.UseEnvironment("Test");

    //        builder.ConfigureServices(services =>
    //        {
    //            var descriptor = services.SingleOrDefault(
    //                d => d.ServiceType == typeof(AppDbContext));

    //            if (descriptor != null)
    //                services.Remove(descriptor);

    //            services.AddDbContext<AppDbContext>(opts =>
    //            {
    //                opts.UseInMemoryDatabase("InMemoryDbForTesting");
    //            });

    //            var provide = services.BuildServiceProvider();

    //            using var scope = provide.CreateScope();
    //            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    //            db.Database.EnsureDeleted();
    //            db.Database.EnsureCreated();

    //            //db.Products.Add(new Product
    //            //{
    //            //    Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
    //            //    Name = "Produto Teste",
    //            //    Description = "Descrição teste",
    //            //    Price = 50,
    //            //    CreatedAt = DateTime.UtcNow
    //            //});

    //            db.SaveChanges();
    //        });
    //        return base.CreateHost(builder);
    //    }
    //}
}
