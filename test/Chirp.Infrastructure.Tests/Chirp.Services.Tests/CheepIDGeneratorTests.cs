using Chirp.Core;
using Chirp.Database;
using Microsoft.EntityFrameworkCore;

namespace Chirp.Services.Tests;

/// <summary>
/// Tests concerning the CheepIDGenerator class
/// </summary>
public class CheepIDGeneratorTests
{
    /// <summary>
    /// Test that the GetNextCheepsId-function properly returns the intended next Cheep-Id
    /// </summary>
    [Fact]
    public void GetNextCheepsIdTest()
    {
        // Arrange
        var dbCtxOptions = new DbContextOptionsBuilder<ChirpDBContext>()
            .UseSqlite($"Data Source=cheepidtest.db")
            .Options;

        var dbContext = new ChirpDBContext(dbCtxOptions);
        dbContext.Database.EnsureCreated();
        if (!dbContext.Cheeps.Any())
        {
            var cheep1 = new Cheep() { CheepId = 42 };
            var cheep2 = new Cheep() { CheepId = 1337 };
            var cheep3 = new Cheep() { CheepId = 19 };
            dbContext.Cheeps.AddRange([cheep1, cheep2, cheep3]);
            dbContext.SaveChanges();
        }

        var expected = 1338;

        // Act
        var actual = CheepIDGenerator.GetNextCheepsId(dbContext);

        // Assert
        Assert.Equal(expected, actual);
    }
}
