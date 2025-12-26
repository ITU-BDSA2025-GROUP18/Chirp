using System.Diagnostics;
using System.Text.RegularExpressions;
using Chirp.Core;
using Chirp.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace Chirp.Web.Tests;

[TestFixture]
public class UiDrivenE2ETests : PageTest
{
    private const string _url = "https://localhost:5001";
    private Process _serverProcess;
    private ChirpDBContext _dbContext;

    // Helper function
    private static string GetWorkingDir()
    {
        var dir = Directory.GetCurrentDirectory();
        dir = Directory.GetParent(dir)!.FullName;
        dir = Directory.GetParent(dir)!.FullName;
        dir = Directory.GetParent(dir)!.FullName;
        dir = Directory.GetParent(dir)!.FullName;
        dir = Directory.GetParent(dir)!.FullName;
        return Path.Combine(dir, "src", "Chirp.Web", "bin", "Debug", "net9.0");
    }

    // Setup and teardown
    [OneTimeSetUp]
    public async Task Init()
    {
        // Get Chirp.Web.dll dir
        var dir = GetWorkingDir();

        // Initialise dbContext
        var options = new DbContextOptionsBuilder<ChirpDBContext>()
            .UseSqlite($"Data Source={Path.Combine(dir, "Data", "playwright.db")}")
            .Options;
        _dbContext = new ChirpDBContext(options);

        // Initialise _serverProcess
        _serverProcess = new Process
        {
            StartInfo =
            {
                WorkingDirectory = dir,
                FileName = Path.Combine(dir, "Chirp.Web"),
                Arguments = $"--urls {_url} --playwright",
                RedirectStandardOutput = true
            }
        };

        // Start process
        _serverProcess.Start();
    }

    [SetUp]
    public async Task Setup()
    {
        // Before every test, go to the front page
        await Page.GotoAsync(_url);
    }

    [OneTimeTearDown]
    public async Task Cleanup()
    {
        // Tear down server
        _serverProcess.Kill();
        _serverProcess.Dispose();

        // Re-initialise dbContext (for some reason, the dbContext can't modify data at this point)
        // (I'm guessing that it's because it was created while the website held the write-lock...)
        var options = new DbContextOptionsBuilder<ChirpDBContext>()
            .UseSqlite($"Data Source={Path.Combine(GetWorkingDir(), "Data", "playwright.db")}")
            .Options;
        _dbContext = new ChirpDBContext(options);

        // Delete test-user
        var testUser = await _dbContext.Authors.Where(a => a.UserName == "Test").FirstOrDefaultAsync();
        if (testUser != null)
        {
            _dbContext.Authors.Remove(testUser);
            await _dbContext.SaveChangesAsync();
        }

        // Tear down dbContext
        _dbContext.Dispose();
    }


    // Test cases. Empirical testing suggests that these are run in alphabetical order...
    [Test]
    public async Task BasicRedirectionsTest()
    {
        Console.Write("e2e-BasicRedirectionsTest");

        // --------------- NAV BAR ---------------
        await Page.GetByRole(AriaRole.Link, new PageGetByRoleOptions { Name = "Register" }).ClickAsync();
        await Expect(Page).ToHaveURLAsync(new Regex("/Identity/Account/Register"));

        await Page.GetByRole(AriaRole.Link, new PageGetByRoleOptions { Name = "Public timeline" }).ClickAsync();
        await Expect(Page).ToHaveURLAsync(_url);

        await Page.GetByRole(AriaRole.Link, new PageGetByRoleOptions { Name = "Login" }).ClickAsync();
        await Expect(Page).ToHaveURLAsync(new Regex("/Identity/Account/Login"));


        // --------------- CHIRP PICTURE ICON ---------------
        await Page.GetByRole(AriaRole.Img, new PageGetByRoleOptions { Name = "Icon1" }).ClickAsync();
        await Expect(Page).ToHaveURLAsync(_url);


        // --------------- PAGINATION FOOTER ---------------
        await Page.GetByText("Next Page").ClickAsync();
        await Expect(Page).ToHaveURLAsync(new Regex("\\?page=2"));

        await Page.GetByText("Previous Page").ClickAsync();
        await Expect(Page).ToHaveURLAsync(new Regex("\\?page=1"));
    }

    [Test]
    public async Task CreateUserTest()
    {
        Console.Write("e2e-CreateUserTest");

        // Assert user 'Test' does not exist
        var query = _dbContext.Authors.Where(a => a.UserName == "Test");
        var testUser = await query.FirstOrDefaultAsync();
        Assert.That(testUser, Is.Null);

        // Navigate to registration page
        await Page.GetByRole(AriaRole.Link, new PageGetByRoleOptions { Name = "Register" }).ClickAsync();

        // Fill information
        await Page.GetByRole(AriaRole.Textbox, new PageGetByRoleOptions { Name = "Username" }).FillAsync("Test");
        await Page.GetByRole(AriaRole.Textbox, new PageGetByRoleOptions { Name = "Email" }).FillAsync("test@user.ex");
        await Page.GetByRole(AriaRole.Textbox, new PageGetByRoleOptions { Name = "Password", Exact = true })
            .FillAsync("TestPassword1!");
        await Page.GetByRole(AriaRole.Textbox, new PageGetByRoleOptions { Name = "Confirm Password" })
            .FillAsync("TestPassword1!");

        // Click 'Register'
        await Page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Register" }).ClickAsync();

        // Assert user 'Test' now exists w/ correct info
        testUser = await query.FirstOrDefaultAsync();
        Assert.That(testUser, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(testUser.UserName, Is.EqualTo("Test"));
            Assert.That(testUser.Email, Is.EqualTo("test@user.ex"));
            Assert.That(testUser.EmailConfirmed, Is.False);
        });

        // Confirm e-mail
        await Page.GetByRole(AriaRole.Link, new PageGetByRoleOptions { Name = "Click here to confirm your" })
            .ClickAsync();

        // Assert that e-mail is now confirmed
        testUser = await query.FirstOrDefaultAsync();
        Assert.That(testUser, Is.Not.Null);
        Assert.That(testUser.EmailConfirmed, Is.False);
    }

    /// <summary>
    /// TODO: WORK IN PROGRESS. DOES NOT ASSERT ANYTHING ATM
    /// </summary>
    [Test]
    public async Task LoginUserTest()
    {
        Console.WriteLine("e2e-LoginUserTest");

        await Page.GetByRole(AriaRole.Link, new PageGetByRoleOptions { Name = "Login" }).ClickAsync();

        await Page.GetByRole(AriaRole.Textbox, new PageGetByRoleOptions { Name = "Email" }).FillAsync("test@user.ex");
        await Page.GetByRole(AriaRole.Textbox, new PageGetByRoleOptions { Name = "Password" })
            .FillAsync("WrongPassword1!");
        await Page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Log in" }).ClickAsync();

        var invalidText = Page.GetByText("Invalid login attempt.");
        Assert.That(invalidText, Is.Not.Null);
    }

    /*
     * Asserts that a newly created user who posts to the website can view their posted cheeps in the About Me page
     */
    [Test]
    public async Task LoginUserSeePostedCheepsTest()
    {
        Console.WriteLine("e2e-LoginUserSeePostedCheepsTest");

        // Arrange
        await CreateTestUser("TestE2E");

        // Act
        await Page.Locator("#Message").ClickAsync();
        await Page.Locator("#Message").FillAsync("Test");
        await Page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Share" }).ClickAsync();
        await Page.GetByRole(AriaRole.Link, new PageGetByRoleOptions { Name = "About me" }).ClickAsync();

        // Assert
        await Expect(Page.GetByRole(AriaRole.Heading, new PageGetByRoleOptions { Name = "Your posted cheeps:" }))
            .ToBeVisibleAsync();
    }

    /*
     * This test asserts that when "Forget Me" button is clicked, the user is absolutely removed from all tables in the db where the user would be: Authors, Cheeps, Followers.
     */
    [Test]
    public async Task LoginUserForgetMeButtonCheckDbTest()
    {
        Console.WriteLine("e2e-LoginUserForgetMeButtonCheckDbTest");

        // Arrange
        await CreateTestUser("TestE2E");
        var authorId = await _dbContext.Authors
            .Where(a => a.UserName == "TestE2E")
            .Select(a => a.Id)
            .FirstAsync();

        // Act
        await Page.GetByRole(AriaRole.Link, new PageGetByRoleOptions { Name = "About me" }).ClickAsync();
        await Page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Forget Me!" }).ClickAsync();

        // Queries for DB assertions
        var testUser = await _dbContext.Authors
            .FirstOrDefaultAsync(a => a.Id == authorId);
        var cheepUser = await _dbContext.Cheeps
            .FirstOrDefaultAsync(c => c.Author.Id == authorId);
        var followerUser = await _dbContext.Followers
            .FirstOrDefaultAsync(f =>
                f.FollowedAuthorId == authorId ||
                f.FollowingAuthorId == authorId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(testUser, Is.Null); // Assert user is deleted in users table
            Assert.That(cheepUser, Is.Null); // Assert user is deleted in cheeps table
            Assert.That(followerUser, Is.Null); // Assert user is deleted in follower table
        });
    }

    /*
     * Test that asserts a user following another displays he follows them
     */
    [Test]
    public async Task UserFollowingDisplaysInAboutMePageTest()
    {
        Console.WriteLine("e2e-UserFollowingDisplaysInAboutMePageTest");

        // Arrange
        await CreateTestUser("Test1");
        await Page.Locator("#Message").ClickAsync();
        await Page.Locator("#Message").FillAsync("Test");
        await Page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Share" }).ClickAsync();
        await Page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Logout [Test1]" }).ClickAsync();

        await CreateTestUser("Test2");
        await Page.GotoAsync("https://localhost:5001/Users/Test1");

        // Act
        await Page
            .GetByRole(AriaRole.Listitem)
            .Filter(new LocatorFilterOptions { HasText = "Test1 Follow Test" })
            .GetByRole(AriaRole.Button)
            .First
            .ClickAsync();

        await Page.GetByRole(AriaRole.Link, new PageGetByRoleOptions { Name = "About me" }).ClickAsync();

        // Assert
        await Expect(Page.GetByRole(AriaRole.Link, new PageGetByRoleOptions { Name = "Test1" }).First)
            .ToBeVisibleAsync();
    }

    /*
     * Helper function used in multiple tests for creating a test user, and logging in.
     */
    private async Task CreateTestUser(string username)
    {
        await Page.GetByRole(AriaRole.Link, new PageGetByRoleOptions { Name = "Register" }).ClickAsync();
        await Page.GetByRole(AriaRole.Textbox, new PageGetByRoleOptions { Name = "Username" }).ClickAsync();
        await Page.GetByRole(AriaRole.Textbox, new PageGetByRoleOptions { Name = "Username" }).FillAsync(username);
        await Page.GetByRole(AriaRole.Textbox, new PageGetByRoleOptions { Name = "Username" }).PressAsync("Tab");
        await Page.GetByRole(AriaRole.Textbox, new PageGetByRoleOptions { Name = "Email" })
            .FillAsync(username + "@itu.dk");
        await Page.GetByRole(AriaRole.Textbox, new PageGetByRoleOptions { Name = "Email" }).PressAsync("Tab");
        await Page.GetByRole(AriaRole.Textbox, new PageGetByRoleOptions { Name = "Password", Exact = true })
            .FillAsync("TestE2E@itu.dk");
        await Page.GetByRole(AriaRole.Textbox, new PageGetByRoleOptions { Name = "Password", Exact = true })
            .PressAsync("Tab");
        await Page.GetByRole(AriaRole.Textbox, new PageGetByRoleOptions { Name = "Confirm Password" })
            .FillAsync("TestE2E@itu.dk");
        await Page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Register" }).ClickAsync();
        await Page.GotoAsync(
            "https://localhost:5001/Identity/Account/RegisterConfirmation?email=" + username + "@itu.dk&returnUrl=%2F");
        await Page.GetByRole(AriaRole.Link, new PageGetByRoleOptions { Name = "Click here to confirm your" })
            .ClickAsync();
        await Page.GetByRole(AriaRole.Link, new PageGetByRoleOptions { Name = "Login" }).ClickAsync();
        await Page.GetByRole(AriaRole.Textbox, new PageGetByRoleOptions { Name = "Email" }).ClickAsync();
        await Page.GetByRole(AriaRole.Textbox, new PageGetByRoleOptions { Name = "Email" })
            .FillAsync(username + "@itu.dk");
        await Page.GetByRole(AriaRole.Textbox, new PageGetByRoleOptions { Name = "Email" }).PressAsync("Tab");
        await Page.GetByRole(AriaRole.Textbox, new PageGetByRoleOptions { Name = "Password" })
            .FillAsync("TestE2E@itu.dk");
        await Page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Log in" }).ClickAsync();
    }
}
