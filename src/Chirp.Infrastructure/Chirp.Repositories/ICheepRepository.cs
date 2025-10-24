namespace Chirp.Repositories;

public interface ICheepRepository
{
    public Task<List<CheepDTO>> GetCheepsAsync(int page);
    public Task<List<CheepDTO>> GetCheepsFromAuthorAsync(string author, int page);
    public Task<AuthorDTO?> GetAuthorFromNameAsync(string name);
}
