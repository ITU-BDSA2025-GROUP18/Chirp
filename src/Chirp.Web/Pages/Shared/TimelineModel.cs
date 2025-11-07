using System.ComponentModel.DataAnnotations;
using Chirp.Core.DTOS;
using Chirp.Database;
using Chirp.Repositories;
using Chirp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chirp.Web.Pages.Shared;

public class TimelineModel(ICheepRepository repository, ChirpDBContext dbContext) : PageModel
{
    protected readonly ICheepRepository Repository = repository;
    protected readonly ChirpDBContext DbContext = dbContext;
    public List<CheepDTO> Cheeps { get; set; } = [];

    [BindProperty]
    [StringLength(160, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
        MinimumLength = 1)]
    public required string Message { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return RedirectToPage();
        }

        var author = await Repository.GetAuthorFromNameAsync(User.Identity!.Name!);
        var cheepId = CheepIDGenerator.GetNextCheepsId(DbContext);
        await Repository.PostCheepAsync(author!, cheepId, Message!);

        return RedirectToPage();
    }
}
