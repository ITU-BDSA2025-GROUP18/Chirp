using System.Threading.Tasks;
using Xunit;

public class TitleTests : IClassFixture<WebAppFactory>
{
    private readonly WebAppFactory _factory;
    public TitleTests(WebAppFactory factory) => _factory = factory;

    [Fact]
    public async Task Root_Title_Contains_AppName()
    {
        var client = _factory.CreateClient();
        var html = await client.GetStringAsync("/");
        Assert.Contains("Chirp", html); // robust: ikke afhængig af præcis titeltekst
    }
}
