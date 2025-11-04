using System.Threading.Tasks;
using Xunit;

public class CharsetTests : IClassFixture<WebAppFactory>
{
    private readonly WebAppFactory _factory;
    public CharsetTests(WebAppFactory factory) => _factory = factory;

    [Fact]
    public async Task Root_Uses_Utf8()
    {
        var client = _factory.CreateClient();
        var res = await client.GetAsync("/");
        res.EnsureSuccessStatusCode();
        var charset = res.Content.Headers.ContentType?.CharSet ?? "";
        Assert.Contains("utf", charset.ToLower());
    }
}
