using Chirp.Database;
using Chirp.Repositories;
using Chirp.Web.Pages.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Chirp.Web.Pages;

public class PublicModel(ChirpDBContext dbContext, IAuthorRepository authorRepo, ICheepRepository cheepRepo, IFollowerRepository followerRepo)
    : TimelineModel(dbContext, authorRepo, cheepRepo, followerRepo)
{
    public int CheepsCount;

    public async Task<ActionResult> OnGet([FromQuery] int page = 1)
    {
        Cheeps = await CheepRepo.GetCheepsAsync(page);
        CheepsCount = CheepRepo.GetCheepsCountAsync().Result;
        var principal = await AuthorRepo.GetAuthorFromNameAsync(User.Identity!.Name!);
        var followerStringSet = new HashSet<string> { };
        if (principal != null)
        {
            var followerSet = FollowerRepo.AuthorFollowing(principal).Result;
            foreach (var follower in followerSet) followerStringSet.Add(follower.FollowedAuthorName);
        }

        Following = followerStringSet;
        return Page();
    }
}
