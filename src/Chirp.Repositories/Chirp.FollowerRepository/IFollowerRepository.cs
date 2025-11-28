using Chirp.Core;

namespace Chirp.Repositories.FollowerRepository;

public interface IFollowerRepository
{
    public Task<int> FollowAsync(Author followingAuthor, Author followedAuthor);
    public Task<int> UnfollowAsync(Author followingAuthor, Author followedAuthor);
    public Task<HashSet<Followers>> AuthorFollowing(Author followingAuthor);
    public Task<int> AuthorFollowersCount(Author author);
}
