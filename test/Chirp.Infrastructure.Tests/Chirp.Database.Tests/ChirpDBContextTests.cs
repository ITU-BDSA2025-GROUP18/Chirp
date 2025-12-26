using Chirp.Core;
using Microsoft.EntityFrameworkCore;

namespace Chirp.Database.Tests;

/// <summary>
/// Tests concerning the ChirpDBContext class
/// </summary>
public class ChirpDBContextTests
{
    /// <summary>
    /// Test that a ChirpDBContext is properly created, e.g. that the created
    /// database context contains empty DbSets for Authors, Cheeps and Followers.
    /// </summary>
    [Fact]
    public void CreateChirpDBCtxTest()
    {
        // Arrange
        var dbCtxOptions = new DbContextOptionsBuilder<ChirpDBContext>()
            .UseSqlite($"Data Source=chirpdbctxtest.db")
            .Options;

        // Act
        var dbContext = new ChirpDBContext(dbCtxOptions);

        dbContext.Database.EnsureCreated();

        // Assert
        Assert.NotNull(dbContext);
        Assert.NotNull(dbContext.Authors);
        Assert.NotNull(dbContext.Cheeps);
        Assert.NotNull(dbContext.Followers);
        Assert.Empty(dbContext.Authors);
        Assert.Empty(dbContext.Cheeps);
        Assert.Empty(dbContext.Followers);

        dbContext.Database.EnsureDeleted();
    }

    /// <summary>
    /// Test whether entities are properly added to an instance of a ChirpDBContext
    /// </summary>
    [Fact]
    public void AddEntitiesToChirpDBCtxTest()
    {
        // Arrange
        var dbCtxOptions = new DbContextOptionsBuilder<ChirpDBContext>()
            .UseSqlite($"Data Source=chirpdbctxtest2.db")
            .Options;

        var dbContext = new ChirpDBContext(dbCtxOptions);
        dbContext.Database.EnsureCreated();

        var author1 = new Author()
        {
            Id = "abc",
            UserName = "TestAuthor",
            Email = "test@email.ex"
        };

        var author2 = new Author()
        {
            Id = "def",
            UserName = "TestAuthor2",
            Email = "test2@email2.ex"
        };

        var cheep = new Cheep()
        {
            CheepId = 67,
            Text = "Test message",
            TimeStamp = DateTime.MinValue
        };

        var followers = new Followers()
        {
            FollowingAuthorId = "abc",
            FollowingAuthorName = "TestAuthor",
            FollowedAuthorId = "def",
            FollowedAuthorName = "TestAuthor2"
        };

        // Act
        dbContext.Authors.AddRange([author1, author2]);
        dbContext.Cheeps.Add(cheep);
        dbContext.Followers.Add(followers);
        dbContext.SaveChanges();

        // Assert
        Assert.NotNull(author1);
        Assert.NotNull(author2);
        Assert.NotNull(cheep);
        Assert.NotNull(followers);
        Assert.Contains(author1, dbContext.Authors);
        Assert.Contains(author2, dbContext.Authors);
        Assert.Contains(cheep, dbContext.Cheeps);
        Assert.Contains(followers, dbContext.Followers);

        dbContext.Database.EnsureDeleted();
    }

    /// <summary>
    /// Test whether entities are properly removed from an instance of a ChirpDBContext
    /// </summary>
    [Fact]
    public void RemoveEntitiesFromChirpDBCtxTest()
    {
        // Arrange
        var dbCtxOptions = new DbContextOptionsBuilder<ChirpDBContext>()
            .UseSqlite($"Data Source=chirpdbctxtest3.db")
            .Options;

        var dbContext = new ChirpDBContext(dbCtxOptions);
        dbContext.Database.EnsureCreated();

        var author1 = new Author()
        {
            Id = "abc",
            UserName = "TestAuthor",
            Email = "test@email.ex"
        };

        var author2 = new Author()
        {
            Id = "def",
            UserName = "TestAuthor2",
            Email = "test2@email2.ex"
        };

        var cheep = new Cheep()
        {
            CheepId = 67,
            Text = "Test message",
            TimeStamp = DateTime.MinValue
        };

        var followers = new Followers()
        {
            FollowingAuthorId = "abc",
            FollowingAuthorName = "TestAuthor",
            FollowedAuthorId = "def",
            FollowedAuthorName = "TestAuthor2"
        };

        dbContext.Authors.AddRange([author1, author2]);
        dbContext.Cheeps.Add(cheep);
        dbContext.Followers.Add(followers);
        dbContext.SaveChanges();

        // Act
        dbContext.Authors.Remove(author1);
        dbContext.Authors.Remove(author2);
        dbContext.Cheeps.Remove(cheep);
        dbContext.Followers.Remove(followers);
        dbContext.SaveChanges();

        // Assert
        Assert.NotNull(author1);
        Assert.NotNull(author2);
        Assert.NotNull(cheep);
        Assert.NotNull(followers);
        Assert.DoesNotContain(author1, dbContext.Authors);
        Assert.DoesNotContain(author2, dbContext.Authors);
        Assert.DoesNotContain(cheep, dbContext.Cheeps);
        Assert.DoesNotContain(followers, dbContext.Followers);

        dbContext.Database.EnsureDeleted();
    }
}
