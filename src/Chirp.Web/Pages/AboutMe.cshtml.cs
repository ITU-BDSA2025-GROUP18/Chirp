using Chirp.Core.DTOs;
using Chirp.Database;
using Chirp.Repositories.AuthorRepository;
using Chirp.Repositories.CheepRepository;
using Chirp.Repositories.FollowerRepository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chirp.Web.Pages;

public class AboutMeModel(ChirpDBContext dbContext, IAuthorRepository authorRepo, ICheepRepository cheepRepo, IFollowerRepository followerRepo) : PageModel
{
    protected readonly ChirpDBContext DbContext = dbContext;
    protected readonly IAuthorRepository AuthorRepo = authorRepo;
    protected readonly ICheepRepository CheepRepo = cheepRepo;
    protected readonly IFollowerRepository FollowerRepo = followerRepo;
    public List<CheepDTO> Cheeps { get; set; } = [];
    public required AuthorDTO PersonalData;

    public async Task<ActionResult> OnGet(string author)
    {
#pragma warning disable CS8601 // Possible null reference assignment.
        PersonalData = await AuthorRepo.GetPersonalDataAsync(User.Identity!.Name!);
#pragma warning restore CS8601 // Possible null reference assignment.

        Cheeps = await CheepRepo.GetAllCheepsFromAuthorsAsync(new HashSet<string> { User.Identity!.Name! });
        return Page();
    }

    public async Task<IActionResult> OnPostForgetMeAsync()
    {
        var author = await AuthorRepo.GetAuthorFromNameAsync(User.Identity!.Name!);

        if (author == null) return RedirectToPage();

        await CheepRepo.ForgetMeAsync(author);

        await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
        await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
        await HttpContext.SignOutAsync(IdentityConstants.TwoFactorUserIdScheme);
        await HttpContext.SignOutAsync();

        return Redirect("/");
    }
}
