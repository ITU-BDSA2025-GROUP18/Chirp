using Microsoft.AspNetCore.Mvc;
using Chirp.Repositories;

namespace Chirp.Web.Pages;

public class PublicModel : TimelineModel //All queries
{
    public int CheepsCount;

    public PublicModel(ICheepRepository repository) : base(repository) { }

    public async Task<ActionResult> OnGet([FromQuery] int page = 1)
    {
        Cheeps = await _repository.GetCheepsAsync(page);
        CheepsCount = _repository.GetCheepsCountAsync().Result;
        return Page();
    }
}
