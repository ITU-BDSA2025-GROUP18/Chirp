using System.Diagnostics;
using System.Text.RegularExpressions;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace Chirp.Web.Tests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class PlaywrightE2EUserCheepTest : PageTest
{
    [Test]
    public async Task BasicRedirectionsTest()
    {
        await Page.GotoAsync("https://bdsagroup18chirpweb.azurewebsites.net/");
    }
}
