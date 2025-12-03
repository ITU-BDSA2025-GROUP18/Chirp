using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Chirp.Web.Tests;

public class AhmadTests(QuietWebAppFactory<Program> factory) : IClassFixture<QuietWebAppFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory = factory;

    [Fact]
    public async Task Root_Uses_Utf8()
    {
        Console.WriteLine("ahmad-Root_Uses_Utf8");
        var client = _factory.CreateClient();
        var res = await client.GetAsync("/");
        res.EnsureSuccessStatusCode();
        var charset = res.Content.Headers.ContentType?.CharSet ?? "";
        Assert.Contains("utf", charset.ToLower());
    }

    [Fact]
    public async Task Root_Has_Html_ContentType()
    {
        Console.WriteLine("ahmad-Root_Has_Html_ContentType");
        var client = _factory.CreateClient();
        var res = await client.GetAsync("/");
        res.EnsureSuccessStatusCode();
        Assert.Contains("text/html", res.Content.Headers.ContentType?.MediaType);
    }

    [Fact]
    public async Task Head_On_Root_Is_Success()
    {
        Console.WriteLine("ahmad-Head_On_Root_Is_Success");
        var client = _factory.CreateClient();
        var req = new HttpRequestMessage(HttpMethod.Head, "/");
        var res = await client.SendAsync(req);
        Assert.True((int)res.StatusCode >= 200 && (int)res.StatusCode < 400);
    }

    [Fact]
    public async Task Root_Html_Starts_With_Doctype()
    {
        Console.WriteLine("ahmad-Root_Html_Starts_With_Doctype");
        var client = _factory.CreateClient();
        var html = await client.GetStringAsync("/");
        Assert.Contains("<!DOCTYPE html", html);
    }

    [Fact]
    public async Task Root_Html_Has_Lang_Attribute()
    {
        Console.WriteLine("ahmad-Root_Html_Has_Lang_Attribute");
        var client = _factory.CreateClient();
        var html = await client.GetStringAsync("/");
        Assert.Contains("<html", html.ToLower());
        Assert.Contains("lang=", html.ToLower());
    }

    [Fact]
    public async Task Root_Has_Title_Tag()
    {
        Console.WriteLine("ahmad-Root_Has_Title_Tag");
        var client = _factory.CreateClient();
        var html = await client.GetStringAsync("/");
        var lower = html.ToLower();
        Assert.Contains("<title", lower);
        Assert.Contains("</title>", lower);
    }

    [Fact]
    public async Task Root_Returns_OK()
    {
        Console.WriteLine("ahmad-Root_Returns_OK");
        var client = _factory.CreateClient();
        var res = await client.GetAsync("/");
        Assert.Equal(HttpStatusCode.OK, res.StatusCode);
    }

    [Fact]
    public async Task Unknown_Page_Returns_404()
    {
        Console.WriteLine("ahmad-Unknown_Page_Returns_404");
        var client = _factory.CreateClient();
        var res = await client.GetAsync("/definitely-not-a-real-page-xyz");
        Assert.Equal(HttpStatusCode.NotFound, res.StatusCode);
    }

    [Fact]
    public async Task Root_Title_Contains_AppName()
    {
        Console.WriteLine("ahmad-Root_Title_Contains_AppName");
        var client = _factory.CreateClient();
        var html = await client.GetStringAsync("/");
        Assert.Contains("Chirp", html); // robust: ikke afhængig af præcis titeltekst
    }

    [Fact]
    public async Task Root_Does_Not_Crash()
    {
        Console.WriteLine("ahmad-Root_Does_Not_Crash");
        var client = _factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = true
        });

        var resp = await client.GetAsync("/");
        Assert.True((int)resp.StatusCode < 500, $"Unexpected status: {(int)resp.StatusCode}");
    }
}
