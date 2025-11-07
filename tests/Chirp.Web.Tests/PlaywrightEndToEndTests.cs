
using System.Diagnostics;
using System.Text.RegularExpressions;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace Chirp.Web.Tests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class PlaywrightEndToEndTests : PageTest
{
    [Test]
    public async Task BasicRedirectionsTest()
    {
        await Page.GotoAsync("https://bdsagroup18chirpweb.azurewebsites.net/");

        // --------------- NAV BAR ---------------
        await Page.GetByRole(AriaRole.Link, new() { Name = "Register" }).ClickAsync();
        await Expect(Page).ToHaveURLAsync(new Regex("/Identity/Account/Register"));

        await Page.GetByRole(AriaRole.Link, new() { Name = "Public timeline" }).ClickAsync();
        await Expect(Page).ToHaveURLAsync("https://bdsagroup18chirpweb.azurewebsites.net/");

        await Page.GetByRole(AriaRole.Link, new() { Name = "Login" }).ClickAsync();
        await Expect(Page).ToHaveURLAsync(new Regex("/Identity/Account/Login"));


        // --------------- CHIRP PICTURE ICON ---------------
        await Page.GetByRole(AriaRole.Img, new() { Name = "Icon1" }).ClickAsync();
        await Expect(Page).ToHaveURLAsync("https://bdsagroup18chirpweb.azurewebsites.net/");


        // --------------- PAGINATION FOOTER ---------------
        await Page.GetByText("Next Page").ClickAsync();
        await Expect(Page).ToHaveURLAsync(new Regex("\\?page=2"));

        await Page.GetByText("Previous Page").ClickAsync();
        await Expect(Page).ToHaveURLAsync(new Regex("\\?page=1"));
    }
}
