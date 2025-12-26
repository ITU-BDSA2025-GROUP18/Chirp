using System.ComponentModel.DataAnnotations;
using Chirp.Core.DTOs;
using Chirp.Database;
using Chirp.Repositories;
using Chirp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chirp.Web.Pages.Shared;

public class TimelineModel(ChirpDBContext dbContext, IAuthorRepository authorRepo, ICheepRepository cheepRepo, IFollowerRepository followerRepo) : PageModel
{
    protected readonly ChirpDBContext DbContext = dbContext;
    protected readonly IAuthorRepository AuthorRepo = authorRepo;
    protected readonly ICheepRepository CheepRepo = cheepRepo;
    protected readonly IFollowerRepository FollowerRepo = followerRepo;
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

        var author = await AuthorRepo.GetAuthorFromNameAsync(User.Identity!.Name!);
        var cheepId = CheepIDGenerator.GetNextCheepsId(DbContext);
        await CheepRepo.PostCheepAsync(author!, cheepId, Message!);

        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostFollowAsync(string authorName)
    {
        var follower = await AuthorRepo.GetAuthorFromNameAsync(User.Identity!.Name!);
        var followed = await AuthorRepo.GetAuthorFromNameAsync(authorName);

        if (follower == null || followed == null) return RedirectToPage();

        var followerSet = await FollowerRepo.AuthorFollowing(follower);

        var alreadyFollowing = followerSet.Any(f =>
            f.FollowingAuthorId == follower.Id &&
            f.FollowedAuthorId == followed.Id
        );

        if (alreadyFollowing) return RedirectToPage();
        if (follower.Id != followed.Id)
            await FollowerRepo.FollowAsync(follower, followed);

        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostUnfollowAsync(string authorName)
    {
        var follower = await AuthorRepo.GetAuthorFromNameAsync(User.Identity!.Name!);
        var followed = await AuthorRepo.GetAuthorFromNameAsync(authorName);

        if (follower == null || followed == null) return RedirectToPage();

        var followerSet = await FollowerRepo.AuthorFollowing(follower);

        var alreadyFollowing = followerSet.Any(f =>
            f.FollowingAuthorId == follower.Id &&
            f.FollowedAuthorId == followed.Id
        );

        if (!alreadyFollowing) return RedirectToPage();
        if (follower.Id != followed.Id)
            await FollowerRepo.UnfollowAsync(follower, followed);

        return RedirectToPage();
    }
}
