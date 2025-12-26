using Chirp.Database;
using Chirp.Repositories;
using Chirp.Web.Pages.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Chirp.Web.Pages;

public class UserTimelineModel(ChirpDBContext dbContext, IAuthorRepository authorRepo, ICheepRepository cheepRepo, IFollowerRepository followerRepo)
    : TimelineModel(dbContext, authorRepo, cheepRepo, followerRepo)
{
    public int AuthorCheepsCount;

    public async Task<ActionResult> OnGet(string author, [FromQuery] int page = 1)
    {
        var authorsToFetch = new HashSet<string> { author };
        var principal = await AuthorRepo.GetAuthorFromNameAsync(User.Identity!.Name!);

        if (principal != null) // if user is logged in:
        {
            FollowersCt = await FollowerRepo.AuthorFollowersCount(principal);
            var followerSet = FollowerRepo.AuthorFollowing(principal).Result;
            foreach (var follower in followerSet)
                authorsToFetch.Add(follower.FollowedAuthorName);
            FollowingCt = followerSet.Count;
        }
        else // if user is not logged in:
        {
            var viewedAuthor = await AuthorRepo.GetAuthorFromNameAsync(author); // get author object from viewed author

            if (viewedAuthor != null) // if the object is not null
            {
                FollowersCt = await FollowerRepo.AuthorFollowersCount(viewedAuthor); // update hsi follwers count
                var followerSet = FollowerRepo.AuthorFollowing(viewedAuthor).Result; // update followerset
                if (User.Identity!.Name! == author)
                    foreach (var follower in followerSet)
                        authorsToFetch.Add(follower.FollowedAuthorName);
                FollowingCt = followerSet.Count;
            }
        }

        Cheeps = await CheepRepo.GetCheepsFromAuthorsAsync(authorsToFetch, page);

        Following = new HashSet<string>(authorsToFetch.Where(name => name != author));
        AuthorCheepsCount = await CheepRepo.GetCheepsFromAuthorsCountAsync(authorsToFetch);

        return Page();
    }
}
