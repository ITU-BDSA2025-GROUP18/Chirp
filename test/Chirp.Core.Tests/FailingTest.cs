
namespace Chirp.Core.Tests;

/// <summary>
/// TEMPORARY TEST CLASS TO DETERMINE WHETHER CI TESTING WORKS AS INTENDED
/// </summary>
public class FailingTest
{
    /// <summary>
    /// Test that will always fail. The question I seek to answer is,
    /// if a test fails, does the workflow terminate? Or does it not care
    /// and continues business as usual. I'm hoping it's the first option...
    /// </summary>
    [Fact]
    public void FailingTestTest()
    {
        Assert.True(false);
    }
}
