using Chirp.Core;
using Chirp.Database;
using Chirp.Repositories;
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
    public async Task GetCheepsFromAuthorAsyncTest()
    {
        //Lets test if we retrieve a list of cheeps made by a chosen author
        //Keep in mind with our pagination, it returns 32 cheeps a page.

        //Arrange
        var dbContext = SqliteDBContext(); //Using fresh sql database
        var author = new Author { UserName = "Eddie" }; //creates author
        dbContext.Authors.AddRange(author);

        dbContext.Cheeps.AddRange(new Cheep
        {
            Author = author,
            Text = "Eddies cheep",
            TimeStamp = DateTime.UtcNow
        }); //Creates cheep(s) with author, text & time



        await dbContext.SaveChangesAsync();

        var repository = new CheepRepository(dbContext);
        //Act
        var results = await repository.GetCheepsFromAuthorAsync("Eddie", page: 1);
        // We're retrieving all results from eddie's page 1.

        //Assert
        Assert.NotEmpty(results); //Testing that results isnt emmpty
        Assert.All(results, cheep => Assert.Equal("Eddie", cheep.AuthorName));
        //Testing that GetCheepsFromAuthorAsync works with results only having cheeps from specific author
    }

    public async Task GetCheepsAsync()
    {
        //GetCheepsAsync retrieves a paged list of cheeps from not a specific author, but all authors.
        //We will now test if our paging works correctly.

        //Arrange
        var dbContext = SqliteDBContext(); //Using fresh sql database

        dbContext.Authors.Add(new Author { UserName = "Eddie" });
        dbContext.Authors.Add(new Author { UserName = "Vinnie" });

        for (int i = 0; i < 32; i++)
        {
            dbContext.Cheeps.Add(new Cheep
            {
                Text = $"Chirp {i}",
                TimeStamp = DateTime.UtcNow,
                Author = dbContext.Authors.First(a => a.UserName == "Eddie")
            });

        }

        for (int i = 0; i < 5; i++)
        {
            dbContext.Cheeps.Add(new Cheep
            {
                Text = $"Chirp {i}",
                TimeStamp = DateTime.UtcNow,
                Author = dbContext.Authors.First(a => a.UserName == "Vinnie")
            });

        }
        await dbContext.SaveChangesAsync();

        var repository = new CheepRepository(dbContext);

        //Act
        var results = await

        //Assert

    }
}
