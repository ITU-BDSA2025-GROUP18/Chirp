
using System.ComponentModel.DataAnnotations;
using Chirp.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chirp.Web.Pages;

public class TimelineModel : PageModel
{
    protected readonly ICheepRepository _repository;
    public List<CheepDTO> Cheeps { get; set; }

    [BindProperty]
    [StringLength(160, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
    public string? Message { get; set; }

    public TimelineModel(ICheepRepository repository)
    {
        _repository = repository;
        Cheeps = new List<CheepDTO>();
    }

    public async void OnPostAsync()
    {
        await _repository.PostCheepAsync(User.Identity!.Name!, Message!);
    }
}
