using Chirp.Database;
using Microsoft.AspNetCore.Mvc;
using Chirp.Repositories;

namespace Chirp.Web.Pages;

public class UserTimelineModel : TimelineModel //All queries
{
    public int AuthorCheepsCount;

    public UserTimelineModel(ICheepRepository repository, ChirpDBContext dbContext) : base(repository, dbContext) { }

    public async Task<ActionResult> OnGet(string author, [FromQuery] int page = 1)
    {
        Cheeps = await _repository.GetCheepsFromAuthorAsync(author, page);
        AuthorCheepsCount = _repository.GetCheepsFromAuthorCountAsync(author).Result;
        return Page();
    }
}
