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

        if (principal != null) // if user is logged in:
        {
            FollowersCt = await Repository.AuthorFollowersCount(principal);
            var followerSet = Repository.AuthorFollowing(principal).Result;
            foreach (var follower in followerSet)
                authorsToFetch.Add(follower.FollowedAuthorName);
            FollowingCt = followerSet.Count;
        }
        else // if user is not logged in:
        {
            var viewedAuthor = await Repository.GetAuthorFromNameAsync(author); // get author object from viewed author

            if (viewedAuthor != null) // if the object is not null
            {
                Console.WriteLine("DEBUG LOOKOING AH: " + viewedAuthor.Email);
                FollowersCt = await Repository.AuthorFollowersCount(viewedAuthor); // update hsi follwers count
                var followerSet = Repository.AuthorFollowing(viewedAuthor).Result; // update followerset
                if (User.Identity!.Name! == author)
                    foreach (var follower in followerSet)
                        authorsToFetch.Add(follower.FollowedAuthorName);
                FollowingCt = followerSet.Count;
            }
        }

        Cheeps = await Repository.GetCheepsFromAuthorsAsync(authorsToFetch, page);

        Following = new HashSet<string>(authorsToFetch.Where(name => name != author));
        AuthorCheepsCount = await Repository.GetCheepsFromAuthorsCountAsync(authorsToFetch);

        return Page();
    }
}
