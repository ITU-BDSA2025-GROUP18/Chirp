using Chirp.Database;
using Microsoft.EntityFrameworkCore;
using Xunit.Sdk;

namespace Chirp.Razor.Tests;

public class CheepRepositoryUnitTests
{
    private readonly string _dbPath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

    private ChirpDBContext SqliteDBContext() //We are making a fresh database for each test
    {
        var options = new DbContextOptionsBuilder<ChirpDBContext>()
            .UseSqlite($"Data Source={_dbPath}")
            .Options;


        var context = new ChirpDBContext(options);
        context.Database.EnsureCreated();

        return context; //To use for each test in Arrange section
    }

    [Fact]
    public void GetCheepsFromAuthorAsyncTest()
    {
        //Lets test if we retrieve a list of cheeps made by a chosen author
        //Keep in mind with our pagination, it returns 32 cheeps a page.

        //Arrange
        var dbContext = SqliteDBContext();


        //Act

        //Assert

    }
}
