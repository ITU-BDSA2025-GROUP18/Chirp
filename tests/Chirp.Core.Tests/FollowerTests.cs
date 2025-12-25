
namespace Chirp.Core.Tests;

/// <summary>
/// Tests concerning the Followers clas
/// </summary>
public class FollowersTests
{
    /// <summary>
    /// Test that Followers is created correctly, e.g. that Followers'
    /// field values correspond to the values passed to its constructor
    /// </summary>
    [Fact]
    public void CreateFollowersTest()
    {
        // Arrange
        var followers = new Followers()
        {
            FollowingAuthorId = "6",
            FollowingAuthorName = "TestAuthor",
            FollowedAuthorId = "7",
            FollowedAuthorName = "TestAuthor2"
        };

        // No acting...

        // Assert
        Assert.NotNull(followers);
        Assert.Equal("6", followers.FollowingAuthorId);
        Assert.Equal("TestAuthor", followers.FollowingAuthorName);
        Assert.Equal("7", followers.FollowedAuthorId);
        Assert.Equal("TestAuthor2", followers.FollowedAuthorName);
    }
}
