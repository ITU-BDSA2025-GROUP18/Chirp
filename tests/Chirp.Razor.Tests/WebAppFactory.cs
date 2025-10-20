using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Chirp.Razor; // for ChirpDBContext
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

public class WebAppFactory : WebApplicationFactory<Chirp.Razor.Program>
{
    private InMemorySqlite<ChirpDBContext>? _mem;
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Test");
        // remove app registrations for ChirpDBContext to override in tests
        builder.ConfigureServices(services =>
        {
            var toRemove = services
                .Where(d => d.ServiceType == typeof(DbContextOptions<ChirpDBContext>) ||
                            d.ServiceType == typeof(ChirpDBContext))
                .ToList();
            foreach (var d in toRemove) services.Remove(d);
        });
        // more config follows
    }
}




