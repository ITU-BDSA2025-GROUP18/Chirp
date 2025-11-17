using Chirp.Core;
using Chirp.Database;
using Microsoft.AspNetCore.Mvc;
using Chirp.Repositories;
using Chirp.Web.Pages.Shared;

namespace Chirp.Web.Pages;

public class PublicModel(ICheepRepository repository, ChirpDBContext dbContext) : TimelineModel(repository, dbContext) //All queries
{
    public int CheepsCount;

    public async Task<ActionResult> OnGet([FromQuery] int page = 1)
    {
        Cheeps = await Repository.GetCheepsAsync(page);
        CheepsCount = Repository.GetCheepsCountAsync().Result;
        return Page();
    }

    public async Task<IActionResult> OnPostFollowAsync(string authorName)
    {
        Console.WriteLine("FOLLOWING " + authorName);
        var follower = await Repository.GetAuthorFromNameAsync(User.Identity!.Name!);
        var followed = await Repository.GetAuthorFromNameAsync(authorName);
        if (followed != null && follower != null && followed != follower)
        {
            await Repository.FollowAsync(followed,followed);
        }

        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostUnfollowAsync()
    {
        Console.WriteLine("UNFOLLOWING HELGE");
        var follower = await Repository.GetAuthorFromNameAsync(User.Identity!.Name!);
        var followed = await Repository.GetAuthorFromNameAsync("Helge");
        await Repository.UnfollowAsync(follower!,followed!);

        return RedirectToPage();
    }
}
