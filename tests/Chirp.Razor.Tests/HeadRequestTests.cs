using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

public class HeadRequestTests : IClassFixture<WebAppFactory>
{
    private readonly WebAppFactory _factory;
    public HeadRequestTests(WebAppFactory factory) => _factory = factory;

    [Fact]
    public async Task Head_On_Root_Is_Success()
    {
        var client = _factory.CreateClient();
        var req = new HttpRequestMessage(HttpMethod.Head, "/");
        var res = await client.SendAsync(req);
        Assert.True((int)res.StatusCode >= 200 && (int)res.StatusCode < 400);
    }
}
