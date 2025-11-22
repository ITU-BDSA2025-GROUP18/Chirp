using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.Extensions.Options;

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
                ["Authentication:Schemes:GitHub:ClientId"] = "test-client-id",
                ["Authentication:Schemes:GitHub:ClientSecret"] = "test-secret",
                ["Authentication:Schemes:GitHub:SignInScheme"] = "Identity.External"
            };
            cfg.AddInMemoryCollection(dict!);
        });

        builder.ConfigureServices(services =>
        {
            // Dette sikrer at ALLE OAuth handlers (inkl. GitHub) har gyldige dummy-værdier
            services.AddSingleton<IPostConfigureOptions<OAuthOptions>, TestOAuthPostConfigure>();
        });
    }
}
