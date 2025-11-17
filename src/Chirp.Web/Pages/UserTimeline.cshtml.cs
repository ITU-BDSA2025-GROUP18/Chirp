using Chirp.Core;
using Chirp.Database;
using Microsoft.AspNetCore.Mvc;
using Chirp.Repositories;
using Chirp.Web.Pages.Shared;

namespace Chirp.Web.Pages;

public class UserTimelineModel(ICheepRepository repository, ChirpDBContext dbContext)
    : TimelineModel(repository, dbContext) //All queries
{
    public int AuthorCheepsCount;

    public async Task<ActionResult> OnGet(string author, [FromQuery] int page = 1)
    {
        Cheeps = await Repository.GetCheepsFromAuthorAsync(author, page);
        //TODO add following uathor's cheeps appear in cheeps
        foreach (var follower in Following) Cheeps.Union(await Repository.GetCheepsFromAuthorAsync(follower, page));
        AuthorCheepsCount = Repository.GetCheepsFromAuthorCountAsync(author).Result;
        return Page();
    }
}
