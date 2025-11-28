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

    public async Task<ActionResult> OnGet()
    {
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
