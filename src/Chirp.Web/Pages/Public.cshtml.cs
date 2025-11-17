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
        var author = await Repository.GetAuthorFromNameAsync(User.Identity!.Name!);
        var followerStringSet = new HashSet<string> { };
        if (author != null)
        {
            var followerSet = Repository.AuthorFollowing(author).Result;
            foreach (var follower in followerSet) followerStringSet.Add(follower.FollowedAuthorName);
        }

        Following = followerStringSet;
        return Page();
    }

    public async Task<IActionResult> OnPostFollowAsync(string authorName)
    {
        var follower = await Repository.GetAuthorFromNameAsync(User.Identity!.Name!);
        var followed = await Repository.GetAuthorFromNameAsync(authorName);

        if (follower == null || followed == null) return RedirectToPage();

        var followerSet = await Repository.AuthorFollowing(follower);

        var alreadyFollowing = followerSet.Any(f =>
            f.FollowingAuthorId == follower.Id &&
            f.FollowedAuthorId == followed.Id
        );

        if (alreadyFollowing) return RedirectToPage();
        if (follower.Id != followed.Id)
            await Repository.FollowAsync(follower, followed);

        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostUnfollowAsync(string authorName)
    {
        var follower = await Repository.GetAuthorFromNameAsync(User.Identity!.Name!);
        var followed = await Repository.GetAuthorFromNameAsync(authorName);

        if (follower == null || followed == null) return RedirectToPage();

        var followerSet = await Repository.AuthorFollowing(follower);

        var alreadyFollowing = followerSet.Any(f =>
            f.FollowingAuthorId == follower.Id &&
            f.FollowedAuthorId == followed.Id
        );

        if (!alreadyFollowing) return RedirectToPage();
        if (follower.Id != followed.Id)
            await Repository.UnfollowAsync(follower, followed);

        return RedirectToPage();
    }
}
