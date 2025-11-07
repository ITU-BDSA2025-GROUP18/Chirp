using System.ComponentModel.DataAnnotations;
using Chirp.Core.DTOS;
using Chirp.Database;
using Chirp.Repositories;
using Chirp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chirp.Web.Pages;

public class TimelineModel : PageModel
{
    protected readonly ICheepRepository _repository;
    protected readonly ChirpDBContext _dbContext;
    public List<CheepDTO> Cheeps { get; set; }

    [BindProperty]
    [StringLength(160, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
        MinimumLength = 1)]
    public required string Message { get; set; }

    public TimelineModel(ICheepRepository repository, ChirpDBContext dbContext)
    {
        _repository = repository;
        _dbContext = dbContext;
        Cheeps = new List<CheepDTO>();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return RedirectToPage();
        }

        var author = await _repository.GetAuthorFromNameAsync(User.Identity!.Name!);
        var cheepId = CheepIDGenerator.GetNextCheepsId(_dbContext);
        await _repository.PostCheepAsync(author!, cheepId, Message!);

        return RedirectToPage();
    }
}
