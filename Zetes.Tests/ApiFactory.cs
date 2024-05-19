using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Zetes.Tests;

public class ApiFactory:WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll<Zetes.Data.ZetesDBContext>();            
            services.AddScoped<Zetes.Data.ZetesDBContext>(p => {
                var c = new Zetes.Data.ZetesDBContext("Test.db");
                c.Database.EnsureDeleted();
                c.Database.EnsureCreated();
                DBSeeder.Seed(c);
                return c;
            });    
        });
    }
}