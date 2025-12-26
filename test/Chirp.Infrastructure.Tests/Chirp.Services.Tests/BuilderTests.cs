using Chirp.Database;
using Chirp.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Chirp.Services.Tests;

/// <summary>
/// Tests concerning the Builder class
/// </summary>
public class BuilderTests
{
    /// <summary>
    /// Test that the Builder-class' Initialize-function returns a proper WebApplicationBuilder,
    /// containing custom, scoped services of types ChirpDBContext, (I)AuhtorRepostitory, (I)CheepRepository and (I)FollowerRepository
    /// </summary>
    [Fact]
    public void InitializeBuilderTest()
    {
        // Arrange
        var builder = Builder.Initialize([]);

        // Act
        var dbContext = builder.Services.FirstOrDefault(s => s.ServiceType == typeof(ChirpDBContext));
        var authorRepo = builder.Services.FirstOrDefault(s => s.ServiceType == typeof(IAuthorRepository));
        var cheepRepo = builder.Services.FirstOrDefault(s => s.ServiceType == typeof(ICheepRepository));
        var followerRepo = builder.Services.FirstOrDefault(s => s.ServiceType == typeof(IFollowerRepository));

        // Assert
        Assert.NotNull(builder);
        Assert.NotNull(dbContext);
        Assert.NotNull(authorRepo);
        Assert.NotNull(cheepRepo);
        Assert.NotNull(followerRepo);
        Assert.Equal(ServiceLifetime.Scoped, dbContext.Lifetime);
        Assert.Equal(ServiceLifetime.Scoped, authorRepo.Lifetime);
        Assert.Equal(ServiceLifetime.Scoped, cheepRepo.Lifetime);
        Assert.Equal(ServiceLifetime.Scoped, followerRepo.Lifetime);
    }
}
