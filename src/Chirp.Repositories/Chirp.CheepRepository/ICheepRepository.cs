using Chirp.Core;
using Chirp.Core.DTOS;

namespace Chirp.Repositories.CheepRepository;

public interface ICheepRepository
{
    public Task<List<CheepDTO>> GetCheepsAsync(int page);
    public Task<int> GetCheepsCountAsync();
    public Task<int> GetCheepsFromAuthorsCountAsync(IEnumerable<string> authors);
    public Task<List<CheepDTO>> GetCheepsFromAuthorsAsync(IEnumerable<string> authors, int page);
    public Task<Author?> GetAuthorFromNameAsync(string name);
    public Task<Author?> GetAuthorFromEmailAsync(string email);
    public Task<int> PostCheepAsync(Author author, int cheepId, string text);
    public Task<int> FollowAsync(Author followingAuthor, Author followedAuthor);
    public Task<int> UnfollowAsync(Author followingAuthor, Author followedAuthor);
    public Task<HashSet<Followers>> AuthorFollowing(Author followingAuthor);
    public Task<int> AuthorFollowersCount(Author author);
}
