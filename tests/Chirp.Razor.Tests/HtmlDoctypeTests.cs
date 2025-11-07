using System.Threading.Tasks;
using Xunit;

public class HtmlDoctypeTests : IClassFixture<WebAppFactory>
{
    private readonly WebAppFactory _factory;
    public HtmlDoctypeTests(WebAppFactory factory) => _factory = factory;

    [Fact]
    public async Task Root_Html_Starts_With_Doctype()
    {
        var client = _factory.CreateClient();
        var html = await client.GetStringAsync("/");
        Assert.Contains("<!DOCTYPE html", html);
    }
}
