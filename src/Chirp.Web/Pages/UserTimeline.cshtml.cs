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
        var authorsToFetch = new HashSet<string> { author };
        var principal = await Repository.GetAuthorFromNameAsync(User.Identity!.Name!);

        if (principal != null)
        {
            var followerSet = Repository.AuthorFollowing(principal).Result;
            foreach (var follower in followerSet) authorsToFetch.Add(follower.FollowedAuthorName);
        }

        Cheeps = await Repository.GetCheepsFromAuthorsAsync(authorsToFetch, page);

        Following = new HashSet<string>(authorsToFetch.Where(name => name != author));

        AuthorCheepsCount = await Repository.GetCheepsFromAuthorsCountAsync(authorsToFetch);

        return Page();
    }
}
