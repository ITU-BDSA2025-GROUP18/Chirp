
using Chirp.Core;

namespace Chirp.Repositories;

public interface ICheepRepository
{
    public Task<List<CheepDTO>> GetCheepsAsync(int page);
    public Task<int> GetCheepsCountAsync();
    public Task<List<CheepDTO>> GetCheepsFromAuthorAsync(string authorName, int page);
    public Task<int> GetCheepsFromAuthorCountAsync(string authorName);
    public Task<Author?> GetAuthorFromNameAsync(string name);
    public Task<Author?> GetAuthorFromEmailAsync(string email);
    public Task<int> PostCheepAsync(Author author, string text);
}
