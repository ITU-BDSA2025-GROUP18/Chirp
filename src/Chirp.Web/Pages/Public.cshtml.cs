using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Chirp.Repositories;

namespace Chirp.Web.Pages;

public class PublicModel : PageModel //All queries
{
    private readonly ICheepRepository _repository;
    public List<CheepDTO> Cheeps { get; set; }
    public int CheepsCount;

    public PublicModel(ICheepRepository repository)
    {
        _repository = repository;
        Cheeps = new List<CheepDTO>();
    }

    public async Task<ActionResult> OnGet([FromQuery] int page = 1)
    {
        Cheeps = await _repository.GetCheepsAsync(page);
        CheepsCount = _repository.GetCheepsCountAsync().Result;
        return Page();
    }
}
