using System.Globalization;
using Chirp.Core.Helpers;

namespace Chirp.Core.Tests;

/// <summary>
/// Tests concerning helper classes and functions
/// </summary>
public class HelperTests
{
    /// <summary>
    /// Test that the DateFormatter-class' TimeStampToLocalTimeString-function
    /// returns the intended string, properly representing a passed DateTime
    /// </summary>
    [Fact]
    public void TimeStampToLocalTimeStringTest()
    {
        // Arrange
        var nowDateTime = DateTime.Now;
        var expected = nowDateTime.ToLocalTime().ToString(CultureInfo.InvariantCulture);

        // Act
        var actual = DateFormatter.TimeStampToLocalTimeString(nowDateTime);

        // Assert
        Assert.Equal(expected, actual);
    }
}
