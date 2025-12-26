using Chirp.Core;
using Chirp.Core.DTOs;

namespace Chirp.Repositories;

public interface ICheepRepository
{
    public Task<List<CheepDTO>> GetCheepsAsync(int page);
    public Task<int> GetCheepsCountAsync();
    public Task<int> GetCheepsFromAuthorsCountAsync(IEnumerable<string> authors);
    public Task<List<CheepDTO>> GetCheepsFromAuthorsAsync(IEnumerable<string> authors, int page);
    public Task<List<CheepDTO>> GetAllCheepsFromAuthorsAsync(IEnumerable<string> authors);
    public Task<int> PostCheepAsync(Author author, int cheepId, string text);
    public Task<int> ForgetMeAsync(Author author);
}
