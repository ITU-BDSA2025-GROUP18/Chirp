
using Chirp.Database;
using Chirp.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chirp.Web.Pages;

public class AboutMeModel(ICheepRepository repository, ChirpDBContext dbContext) : PageModel
{
    public async Task<ActionResult> OnGet()
    {
        return Page();
    }
}
