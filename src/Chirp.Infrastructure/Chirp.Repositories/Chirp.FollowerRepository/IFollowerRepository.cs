using Chirp.Core;
using Chirp.Core.DTOs;

namespace Chirp.Repositories;

/// <summary>
/// Defines the contract for follower-related data access operations.
/// </summary>
/// <remarks>
/// This interface provides methods to manage and retrieve follower relationships
/// between authors in the Chirp system. It supports retrieving follower lists,
/// counting followers, and creating or removing follow relationships.
/// </remarks>
public interface IFollowerRepository
{
    /// <summary>
    /// Retrieves the set of authors that a given author is following.
    /// </summary>
    /// <param name="followingAuthor">The author whose followings are being retrieved.</param>
    /// <returns>A <see cref="HashSet{Followers}"/> containing the following relationships.</returns>
    public Task<HashSet<Followers>> AuthorFollowing(Author followingAuthor);

    /// <summary>
    /// Retrieves the total number of followers for a given author.
    /// </summary>
    /// <param name="author">The author whose followers are being counted.</param>
    /// <returns>The number of followers the author has.</returns>
    public Task<int> AuthorFollowersCount(Author author);

    /// <summary>
    /// Retrieves a list of authors that a given author is following.
    /// </summary>
    /// <param name="authorName">The username of the author.</param>
    /// <returns>A list of <see cref="FollowerDTO"/> representing the authors being followed.</returns>
    public Task<List<FollowerDTO>> GetAuthorFollowing(string authorName);

    /// <summary>
    /// Retrieves a list of authors who follow a given author.
    /// </summary>
    /// <param name="authorName">The username of the author.</param>
    /// <returns>A list of <see cref="FollowerDTO"/> representing the author's followers.</returns>
    public Task<List<FollowerDTO>> GetAuthorFollowers(string authorName);

    /// <summary>
    /// Creates a follow relationship where one author follows another.
    /// </summary>
    /// <param name="followingAuthor">The author who will follow.</param>
    /// <param name="followedAuthor">The author to be followed.</param>
    /// <returns>An integer indicating the number of state entries written to the database.</returns>
    public Task<int> FollowAsync(Author followingAuthor, Author followedAuthor);

    /// <summary>
    /// Removes an existing follow relationship where one author unfollows another.
    /// </summary>
    /// <param name="followingAuthor">The author who will unfollow.</param>
    /// <param name="followedAuthor">The author to be unfollowed.</param>
    /// <returns>An integer indicating the number of state entries affected.</returns>
    public Task<int> UnfollowAsync(Author followingAuthor, Author followedAuthor);
}
