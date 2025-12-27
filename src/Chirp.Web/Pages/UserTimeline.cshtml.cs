using Chirp.Database;
using Chirp.Repositories;
using Chirp.Web.Pages.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Chirp.Web.Pages;

public class UserTimelineModel(
    ChirpDBContext dbContext,
    IAuthorRepository authorRepo,
    ICheepRepository cheepRepo,
    IFollowerRepository followerRepo)
    : TimelineModel(dbContext, authorRepo, cheepRepo, followerRepo)
{
    public int AuthorCheepsCount;

    public async Task<ActionResult> OnGet(string author, [FromQuery] int page = 1)
    {
        var authorsToFetch = new HashSet<string> { author };

        // get the viewed author
        var viewedAuthor = await AuthorRepo.GetAuthorFromNameAsync(author);

        if (viewedAuthor != null)
        {
            FollowersCt = await FollowerRepo.AuthorFollowersCount(viewedAuthor);
            var followingSet = await FollowerRepo.AuthorFollowing(viewedAuthor);

            // only include following if viewing your own timeline
            if (User.Identity!.Name == author)
                foreach (var f in followingSet)
                    authorsToFetch.Add(f.FollowedAuthorName);

            FollowingCt = followingSet.Count;
        }

        // fetch cheeps
        Cheeps = await CheepRepo.GetCheepsFromAuthorsAsync(authorsToFetch, page);
        AuthorCheepsCount = await CheepRepo.GetCheepsFromAuthorsCountAsync(authorsToFetch);

        // populate current user's followings
        if (User.Identity != null)
        {
            var currentUser = await AuthorRepo.GetAuthorFromNameAsync(User.Identity.Name!);
            if (currentUser != null)
            {
                var currentUserFollowing = await FollowerRepo.AuthorFollowing(currentUser);
                UserFollowing = new HashSet<string>(currentUserFollowing.Select(f => f.FollowedAuthorName));
            }
        }

        return Page();
    }
}
