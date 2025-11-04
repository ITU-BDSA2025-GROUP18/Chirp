using System.Net;
using System.Threading.Tasks;
using Xunit;

public class HttpSmokeTests : IClassFixture<WebAppFactory>
{
    private readonly WebAppFactory _factory;
    public HttpSmokeTests(WebAppFactory factory) => _factory = factory;

    [Fact]
    public async Task Root_Returns_OK()
    {
        var client = _factory.CreateClient();
        var res = await client.GetAsync("/");
        Assert.Equal(HttpStatusCode.OK, res.StatusCode);
    }
}
