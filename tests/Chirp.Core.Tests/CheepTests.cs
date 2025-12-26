
namespace Chirp.Core.Tests;

/// <summary>
/// Tests concerning the Cheep class
/// </summary>
public class CheepTests
{
    /// <summary>
    /// Test that a Cheep is created correctly, e.g. that a cheep's
    /// field values correspond to the values passed to its constructor
    /// </summary>
    [Fact]
    public void CreateCheepTest()
    {
        // Arrange
        var cheep = new Cheep()
        {
            CheepId = 0,
            Text = "Test message",
            TimeStamp = DateTime.MinValue
        };

        // No acting...

        // Assert
        Assert.NotNull(cheep);
        Assert.Equal(0, cheep.CheepId);
        Assert.Equal("Test message", cheep.Text);
        Assert.Equal(DateTime.MinValue, cheep.TimeStamp);
    }
}
