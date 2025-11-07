using Chirp.Database;
using Microsoft.AspNetCore.Mvc;
using Chirp.Repositories;

namespace Chirp.Web.Pages;

public class PublicModel(ICheepRepository repository, ChirpDBContext dbContext) : TimelineModel(repository, dbContext) //All queries
{
    public int CheepsCount;

    public async Task<ActionResult> OnGet([FromQuery] int page = 1)
    {
        Cheeps = await Repository.GetCheepsAsync(page);
        CheepsCount = Repository.GetCheepsCountAsync().Result;
        return Page();
    }
}
