
using Chirp.Core.DTOs;

namespace Chirp.Core.Tests;

/// <summary>
/// Tests concerning the data tansfer object-classes (DTOs)
/// </summary>
public class DTOTests
{
    /// <summary>
    /// Test that an AuthorDTO is created correctly, e.g. that an AuthorDTO's
    /// field values correspond to the values passed to its constructor
    /// </summary>
    [Fact]
    public void CreateAuthorDTOTest()
    {
        // Arrange
        var authorDTO = new AuthorDTO()
        {
            UserName = "TestAuthor",
            Email = "test@email.ex",
            PhoneNumber = "12 34 56 78"
        };

        // No acting...

        // Assert
        Assert.NotNull(authorDTO);
        Assert.Equal("TestAuthor", authorDTO.UserName);
        Assert.Equal("test@email.ex", authorDTO.Email);
        Assert.Equal("12 34 56 78", authorDTO.PhoneNumber);
    }

    /// <summary>
    /// Test that a CheepDTO is created correctly, e.g. that a CheepDTO's
    /// field values correspond to the values passed to its constructor
    /// </summary>
    [Fact]
    public void CreateCheepDTOTest()
    {
        // Arrange
        var cheepDTO = new CheepDTO()
        {
            AuthorName = "TestAuthor",
            Text = "Test message",
            Timestamp = DateTime.MinValue.ToString()
        };

        // No acting...

        // Assert
        Assert.NotNull(cheepDTO);
        Assert.Equal("TestAuthor", cheepDTO.AuthorName);
        Assert.Equal("Test message", cheepDTO.Text);
        Assert.Equal("01/01/0001 00.00.00", cheepDTO.Timestamp);
    }

    /// <summary>
    /// Test that a FollowerDTO is created correctly, e.g. that a FollowerDTO's
    /// field values correspond to the values passed to its constructor
    /// </summary>
    [Fact]
    public void CreateFollowerDTOTest()
    {
        // Arrange
        var followerDTO = new FollowerDTO()
        {
            FollowerName = "TestAuthor"
        };

        // No acting...

        // Assert
        Assert.NotNull(followerDTO);
        Assert.Equal("TestAuthor", followerDTO.FollowerName);
    }
}
