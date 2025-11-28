using Chirp.Database;
using Chirp.Repositories.CheepRepository;
using Chirp.Web.Pages.Shared;
using Microsoft.AspNetCore.Mvc;

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
