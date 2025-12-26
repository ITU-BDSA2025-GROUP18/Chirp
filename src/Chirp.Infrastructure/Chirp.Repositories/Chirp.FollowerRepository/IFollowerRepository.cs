using Chirp.Core;
using Chirp.Core.DTOs;

namespace Chirp.Repositories.FollowerRepository;

public interface IFollowerRepository
{
    // GET
    public Task<HashSet<Followers>> AuthorFollowing(Author followingAuthor);
    public Task<int> AuthorFollowersCount(Author author);

    /// <summary>
    /// Return a list of who a given user is following
    /// </summary>
    public Task<List<FollowerDTO>> GetAuthorFollowing(string authorName);

    /// <summary>
    /// Return a list of a given user's followers
    /// </summary>
    public Task<List<FollowerDTO>> GetAuthorFollowers(string authorName);


    // POST
    public Task<int> FollowAsync(Author followingAuthor, Author followedAuthor);
    public Task<int> UnfollowAsync(Author followingAuthor, Author followedAuthor);
}
