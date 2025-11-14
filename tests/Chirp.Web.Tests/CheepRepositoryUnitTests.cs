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

    [Fact]
    public async Task GetCheepsAsyncTest()
    {
        //GetCheepsAsync retrieves a paged list of cheeps from not a specific author, but all authors.
        //We will now test if our paging works correctly. A page has 32 cheeps.
        //But we will create 37 cheeps. Should return true in two pages
        //which Page 1 has 32 cheeps and Page 2: 5 cheeps.

        //Arrange
        var dbContext = SqliteDBContext(); //Using fresh sql database

        var author1 = new Author { UserName = "Eddie" }; //creates author
        dbContext.Authors.AddRange(author1);

        var author2 = new Author { UserName = "Vinnie" }; //creates author
        dbContext.Authors.AddRange(author2);

        await dbContext.SaveChangesAsync(); //Saves changes(adding authors) to dbContext

        for (int i = 0; i < 32; i++)
        { //Creating cheeps for author Eddie
            dbContext.Cheeps.Add(new Cheep
            {
                Author = author1,
                Text = $"Chirp {i}",
                TimeStamp = DateTime.UtcNow
            });

        }

        for (int i = 0; i < 5; i++)
        { //Creating cheeps for author Vinnie
            dbContext.Cheeps.Add(new Cheep
            {
                Author = author2,
                Text = $"Chirp {i}",
                TimeStamp = DateTime.UtcNow
            });

        }
        await dbContext.SaveChangesAsync();

        var repository = new CheepRepository(dbContext);

        //Act
        //We'll store lists of the cheeps from page 1 & 2 into variables
        var page1 = await repository.GetCheepsAsync(1);
        var page2 = await repository.GetCheepsAsync(2);

        //Assert
        Assert.Equal(32, page1.Count); //Should return true if page1 has 32 cheeps
        Assert.Equal(5, page2.Count); //Should return true if page2 has 5 cheeps.
    }

    [Fact]
    public async Task GetAuthorFromNameAsyncTest()
    {
        //We will test for getting the correct Author just by knowing
        //that Author's name. (An authors name is an authors username)

        //Arrange
        var dbContext = SqliteDBContext(); //Using fresh sql database

        var author = new Author { UserName = "Eddie" }; //creates author
        dbContext.Authors.AddRange(author);

        //Saving changes and adds it to the repository
        await dbContext.SaveChangesAsync();
        var repository = new CheepRepository(dbContext);

        //Act

        var results = await repository.GetAuthorFromNameAsync("Eddie");

        //Assert
        //We'll assert if this name belongs to that author.
        //and that we can get the author from that name.
        Assert.NotNull(results);
        Assert.Equal("Eddie", results.UserName);
    }

    [Fact]
    public async Task GetAuthorFromName_NonExistant_ReturnsNullAsyncTest()
    {
        //We should test that if a name doesn't belong to any author. We should
        //get null. In the case that no author exists with that name.
        //(An authors name is an authors username)

        //Arrange
        var dbContext = SqliteDBContext(); //Using fresh sql database

        var author = new Author { UserName = "Eddie" }; //creates author
        dbContext.Authors.AddRange(author);

        //Saving changes and adds it to the repository
        await dbContext.SaveChangesAsync();
        var repository = new CheepRepository(dbContext);

        //Act

        //Running GetAuthorFromName-method with name "Jack" which we know
        //doesn't belong to any author, to results. Should return null.
        var results = await repository.GetAuthorFromNameAsync("Jack");

        //Assert
        //Testing if it is true that the result(Jack) would not belong to any author
        //and therefore it should return null.
        Assert.Null(results);
    }

    [Fact]
    public async Task GetAuthorFromEmailAsyncTest()
    {
        //We will test for getting the correct Author just by knowing
        //that Author's email

        //Arrange
        var dbContext = SqliteDBContext(); //Using fresh sql database

        //Act

        //Assert
        //We'll assert if this email belongs to that author.
        //and that we can get the author from that email.
    }
}
