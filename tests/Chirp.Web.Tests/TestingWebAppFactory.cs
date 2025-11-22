using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

namespace Chirp.Web.Tests;

public class TestingWebAppFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");

        builder.ConfigureAppConfiguration((ctx, cfg) =>
        {
            var dict = new Dictionary<string, string?>
            {
                // Sørger for at GitHub OAuth ikke kaster pga. manglende nøgler i test
                ["Authentication:Schemes:GitHub:ClientId"] = "test-client-id",
                ["Authentication:Schemes:GitHub:ClientSecret"] = "test-secret",
                // De fleste skabeloner bruger cookies som SignInScheme
                ["Authentication:Schemes:GitHub:SignInScheme"] = "Identity.External"
            };

            cfg.AddInMemoryCollection(dict!);
        });
    }
}
