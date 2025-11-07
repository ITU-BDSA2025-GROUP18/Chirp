using System.Threading.Tasks;
using Xunit;

public class HtmlTitlePresenceTests : IClassFixture<WebAppFactory>
{
    private readonly WebAppFactory _factory;
    public HtmlTitlePresenceTests(WebAppFactory factory) => _factory = factory;

    [Fact]
    public async Task Root_Has_Title_Tag()
    {
        var client = _factory.CreateClient();
        var html = await client.GetStringAsync("/");
        var lower = html.ToLower();
        Assert.Contains("<title", lower);
        Assert.Contains("</title>", lower);
    }
}
