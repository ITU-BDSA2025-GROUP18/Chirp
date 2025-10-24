﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Chirp.Repositories;

namespace Chirp.Web.Pages;

public class PublicModel : PageModel //All queries
{
    private readonly ICheepQueryRepository _repository;
    public List<CheepDTO> Cheeps { get; set; }

    public PublicModel(ICheepQueryRepository repository)
    {
        _repository = repository;
        Cheeps = new List<CheepDTO>();
    }

    public async Task<ActionResult> OnGet([FromQuery] int page = 1)
    {
        Cheeps = await _repository.GetCheepsAsync(page);
        return Page();
    }
}
