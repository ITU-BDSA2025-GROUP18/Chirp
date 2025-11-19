using Chirp.Core;
using Chirp.Database;
using Microsoft.AspNetCore.Mvc;
using Chirp.Repositories;
using Chirp.Web.Pages.Shared;

namespace Chirp.Web.Pages;

public class PublicModel(ICheepRepository repository, ChirpDBContext dbContext)
    : TimelineModel(repository, dbContext) //All queries
{
    public int CheepsCount;

    public async Task<ActionResult> OnGet([FromQuery] int page = 1)
    {
        Cheeps = await Repository.GetCheepsAsync(page);
        CheepsCount = Repository.GetCheepsCountAsync().Result;
        var principal = await Repository.GetAuthorFromNameAsync(User.Identity!.Name!);
        var followerStringSet = new HashSet<string> { };
        if (principal != null)
        {
            var followerSet = Repository.AuthorFollowing(principal).Result;
            foreach (var follower in followerSet) followerStringSet.Add(follower.FollowedAuthorName);
        }

        Following = followerStringSet;
        return Page();
    }
}
