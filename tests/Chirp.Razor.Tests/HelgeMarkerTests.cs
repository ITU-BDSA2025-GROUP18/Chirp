using System.Threading.Tasks;
using Xunit;

public class HelgeMarkerTests : IClassFixture<WebAppFactory>
{
    private readonly WebAppFactory _factory;
    public HelgeMarkerTests(WebAppFactory factory) => _factory = factory;

    [Fact]
    public async Task Root_Contains_Helge_Marker_In_Testing()
    {
        // Denne test er robust mod styling: søger kun efter ordet "Helge"
        var client = _factory.CreateClient();
        var html = await client.GetStringAsync("/");
        Assert.Contains("Helge", html);
    }
}
