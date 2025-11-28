using Chirp.Database;
using Chirp.Repositories.AuthorRepository;
using Chirp.Repositories.CheepRepository;
using Chirp.Web.Pages.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Chirp.Web.Pages;

public class PublicModel(ChirpDBContext dbContext, IAuthorRepository authorRepo, ICheepRepository cheepRepo)
    : TimelineModel(dbContext, authorRepo, cheepRepo)
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
            var followerSet = CheepRepo.AuthorFollowing(principal).Result;
            foreach (var follower in followerSet) followerStringSet.Add(follower.FollowedAuthorName);
        }

        Following = followerStringSet;
        return Page();
    }
}
