using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Chirp.Web.Tests;

public class HomeSmokeTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    public HomeSmokeTests(WebApplicationFactory<Program> factory) => _factory = factory;

    [Fact]
    public async Task Root_Does_Not_Crash()
    {
        var client = _factory.CreateClient(new WebApplicationFactoryClientOptions {
            AllowAutoRedirect = true
        });

        var resp = await client.GetAsync("/");
        Assert.True((int)resp.StatusCode < 500, $"Unexpected status: {(int)resp.StatusCode}");
    }
}
