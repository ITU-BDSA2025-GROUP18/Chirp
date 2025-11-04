using System.Threading.Tasks;
using Xunit;

public class ContentTypeTests : IClassFixture<WebAppFactory>
{
    private readonly WebAppFactory _factory;
    public ContentTypeTests(WebAppFactory factory) => _factory = factory;

    [Fact]
    public async Task Root_Has_Html_ContentType()
    {
        var client = _factory.CreateClient();
        var res = await client.GetAsync("/");
        res.EnsureSuccessStatusCode();
        Assert.Contains("text/html", res.Content.Headers.ContentType?.MediaType);
    }
}
