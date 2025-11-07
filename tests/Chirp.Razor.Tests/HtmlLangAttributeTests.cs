using System.Threading.Tasks;
using Xunit;

public class HtmlLangAttributeTests : IClassFixture<WebAppFactory>
{
    private readonly WebAppFactory _factory;
    public HtmlLangAttributeTests(WebAppFactory factory) => _factory = factory;

    [Fact]
    public async Task Root_Html_Has_Lang_Attribute()
    {
        var client = _factory.CreateClient();
        var html = await client.GetStringAsync("/");
        Assert.Contains("<html", html.ToLower());
        Assert.Contains("lang=", html.ToLower());
    }
}
