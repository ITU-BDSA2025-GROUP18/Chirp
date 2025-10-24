using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Chirp.Repositories;

namespace Chirp.Web.Pages;

public class UserTimelineModel : PageModel //All queries
{
    private readonly ICheepRepository _repository;
    public List<CheepDTO> Cheeps { get; set; }

    public UserTimelineModel(ICheepRepository repository)
    {
        _repository = repository;
        Cheeps = new List<CheepDTO>();
    }

    public async Task<ActionResult> OnGet(string author, [FromQuery] int page = 1)
    {
        Cheeps = await _repository.GetCheepsFromAuthorAsync(author, page);
        return Page();
    }
}
