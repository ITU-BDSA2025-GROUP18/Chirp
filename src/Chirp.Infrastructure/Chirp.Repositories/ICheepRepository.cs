
using Chirp.Core;

namespace Chirp.Repositories;

public interface ICheepRepository
{
    public Task<List<CheepDTO>> GetCheepsAsync(int page);
    public Task<int> GetCheepsCountAsync();
    public Task<List<CheepDTO>> GetCheepsFromAuthorAsync(string author, int page);
    public Task<int> GetCheepsFromAuthorCountAsync(string author);
    public Task<AuthorDTO?> GetAuthorFromNameAsync(string name);
    public Task<AuthorDTO?> GetAuthorFromEmailAsync(string email);
    public Task<int> PostAuthorAsync(string name, string email);
    public Task<int> PostCheepAsync(string authorName, string text);
}
