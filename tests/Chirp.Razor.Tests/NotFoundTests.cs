using System.Net;
using System.Threading.Tasks;
using Xunit;

public class NotFoundTests : IClassFixture<WebAppFactory>
{
    private readonly WebAppFactory _factory;
    public NotFoundTests(WebAppFactory factory) => _factory = factory;

    [Fact]
    public async Task Unknown_Page_Returns_404()
    {
        var client = _factory.CreateClient();
        var res = await client.GetAsync("/definitely-not-a-real-page-xyz");
        Assert.Equal(HttpStatusCode.NotFound, res.StatusCode);
    }
}
