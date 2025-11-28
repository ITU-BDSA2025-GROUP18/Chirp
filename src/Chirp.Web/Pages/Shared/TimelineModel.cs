using System.ComponentModel.DataAnnotations;
using Chirp.Core;
using Chirp.Core.DTOS;
using Chirp.Database;
using Chirp.Repositories;
using Chirp.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chirp.Web.Pages.Shared;

public class TimelineModel(ICheepRepository repository, ChirpDBContext dbContext) : PageModel
{
    protected readonly ICheepRepository Repository = repository;
    protected readonly ChirpDBContext DbContext = dbContext;
    public List<CheepDTO> Cheeps { get; set; } = [];
    public HashSet<string> Following { get; set; } = [];
    public int FollowersCt { get; set; } = 0;
    public int FollowingCt { get; set; } = 0;

    [BindProperty]
    [StringLength(160, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
        MinimumLength = 1)]
    public required string Message { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return RedirectToPage();

        var author = await Repository.GetAuthorFromNameAsync(User.Identity!.Name!);
        var cheepId = CheepIDGenerator.GetNextCheepsId(DbContext);
        await Repository.PostCheepAsync(author!, cheepId, Message!);

        return RedirectToPage();
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
