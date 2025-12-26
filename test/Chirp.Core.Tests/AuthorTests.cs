
namespace Chirp.Core.Tests;

/// <summary>
/// Tests concerning the Author class
/// </summary>
public class AuthorTests
{
    /// <summary>
    /// Test that an Author is created correctly, e.g. that an Author's
    /// field values correspond to the values passed to its constructor
    /// </summary>
    [Fact]
    public void CreateAuthorTest()
    {
        // Arrange
        var author = new Author()
        {
            Id = "TestId",
            UserName = "TestAuthor",
            NormalizedUserName = "TESTAUTHOR",
            Email = "test@email.ex",
            NormalizedEmail = "TEST@EMAIL.EX",
            EmailConfirmed = false,
            PasswordHash = null,
            SecurityStamp = null,
            ConcurrencyStamp = null,
            PhoneNumber = null,
            PhoneNumberConfirmed = false,
            TwoFactorEnabled = false,
            LockoutEnd = null,
            LockoutEnabled = false,
            AccessFailedCount = 0,
            Cheeps = []
        };

        // No acting...

        // Assert
        Assert.NotNull(author);
        Assert.Equal("TestId", author.Id);
        Assert.Equal("TestAuthor", author.UserName);
        Assert.Equal("TESTAUTHOR", author.NormalizedUserName);
        Assert.Equal("test@email.ex", author.Email);
        Assert.Equal("TEST@EMAIL.EX", author.NormalizedEmail);
        Assert.False(author.EmailConfirmed);
        Assert.Null(author.PasswordHash);
        Assert.Null(author.SecurityStamp);
        Assert.Null(author.ConcurrencyStamp);
        Assert.Null(author.PhoneNumber);
        Assert.False(author.PhoneNumberConfirmed);
        Assert.False(author.TwoFactorEnabled);
        Assert.Null(author.LockoutEnd);
        Assert.False(author.LockoutEnabled);
        Assert.Equal(0, author.AccessFailedCount);
        Assert.Empty(author.Cheeps);
    }
}
