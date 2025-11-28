using Chirp.Core;
using Chirp.Core.DTOS;
using Chirp.Database;
using Chirp.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chirp.Web.Pages;

public class AboutMeModel(ICheepRepository repository, ChirpDBContext dbContext) : PageModel
{
    protected readonly ICheepRepository Repository = repository;
    protected readonly ChirpDBContext DbContext = dbContext;
    public List<CheepDTO> Cheeps { get; set; } = [];
    public required Author Author;

    public async Task<ActionResult> OnGet(string author)
    {
        Cheeps = await Repository.GetAllCheepsFromAuthorsAsync(new HashSet<string> { User.Identity!.Name! });
        return Page();
    }

    public async Task<IActionResult> OnPostForgetMeAsync()
    {
        var author = await Repository.GetAuthorFromNameAsync(User.Identity!.Name!);

        if (author == null) return RedirectToPage();

        await Repository.ForgetMeAsync(author);

        await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
        await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
        await HttpContext.SignOutAsync(IdentityConstants.TwoFactorUserIdScheme);
        await HttpContext.SignOutAsync();

        return Redirect("/");
    }
}
