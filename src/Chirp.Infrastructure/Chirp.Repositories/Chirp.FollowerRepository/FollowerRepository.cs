using Chirp.Core;
using Chirp.Core.DTOs;
using Chirp.Database;
using Microsoft.EntityFrameworkCore;

namespace Chirp.Repositories;

/// <summary>
/// Provides follower-related data access operations using Entity Framework Core.
/// </summary>
/// <remarks>
/// This class implements <see cref="IFollowerRepository"/> and interacts with
/// the <see cref="ChirpDBContext"/> to manage and retrieve follower relationships
/// between authors. It supports retrieving follower lists, counting followers,
/// and creating or removing follow relationships.
/// </remarks>
public class FollowerRepository(ChirpDBContext dbContext) : IFollowerRepository
{
    // ============== Get Endpoints ============== //

    /// <summary>
    /// Retrieves the set of authors that a given author is following.
    /// </summary>
    /// <param name="followingAuthor">The author whose followings are being retrieved.</param>
    /// <returns>A <see cref="HashSet{Followers}"/> containing the following relationships.</returns>
    public Task<HashSet<Followers>> AuthorFollowing(Author followingAuthor)
    {
        return Task.FromResult(dbContext.Followers
            .Where(follower => follower.FollowingAuthorId == followingAuthor.Id)
            .ToHashSet());
    }

    /// <summary>
    /// Retrieves the total number of followers for a given author.
    /// </summary>
    /// <param name="author">The author whose followers are being counted.</param>
    /// <returns>The number of followers the author has.</returns>
    public async Task<int> AuthorFollowersCount(Author author)
    {
        var count = await dbContext.Followers
            .Where(follower => follower.FollowedAuthorId == author.Id)
            .CountAsync();

        return count;
    }

    /// <summary>
    /// Retrieves a list of authors that a given author is following.
    /// </summary>
    /// <param name="authorName">The username of the author.</param>
    /// <returns>A list of <see cref="FollowerDTO"/> representing the authors being followed.</returns>
    public async Task<List<FollowerDTO>> GetAuthorFollowing(string authorName)
    {
        var query = dbContext.Followers
            .Where(f => f.FollowingAuthorName == authorName)
            .Select(f => new FollowerDTO
            {
                FollowerName = f.FollowedAuthorName
            });

        return await query.ToListAsync();
    }

    /// <summary>
    /// Retrieves a list of authors who follow a given author.
    /// </summary>
    /// <param name="authorName">The username of the author.</param>
    /// <returns>A list of <see cref="FollowerDTO"/> representing the author's followers.</returns>
    public async Task<List<FollowerDTO>> GetAuthorFollowers(string authorName)
    {
        var query = dbContext.Followers
            .Where(f => f.FollowedAuthorName == authorName)
            .Select(f => new FollowerDTO
            {
                FollowerName = f.FollowingAuthorName
            });

        return await query.ToListAsync();
    }

    // ============== Post Endpoints ============== //

    /// <summary>
    /// Creates a follow relationship where one author follows another.
    /// </summary>
    /// <param name="followingAuthor">The author who will follow.</param>
    /// <param name="followedAuthor">The author to be followed.</param>
    /// <returns>An integer representing the number of state entries written to the database.</returns>
    public async Task<int> FollowAsync(Author followingAuthor, Author followedAuthor)
    {
        dbContext.Followers.Add(new Followers
        {
            FollowingAuthorId = followingAuthor.Id,
            FollowingAuthorName = followingAuthor.UserName,
            FollowedAuthorId = followedAuthor.Id,
            FollowedAuthorName = followedAuthor.UserName
        });

        await AuthorFollowing(followingAuthor);
        return await dbContext.SaveChangesAsync();
    }

    /// <summary>
    /// Removes an existing follow relationship where one author unfollows another.
    /// </summary>
    /// <param name="followingAuthor">The author who will unfollow.</param>
    /// <param name="followedAuthor">The author to be unfollowed.</param>
    /// <returns>An integer indicating the number of state entries affected.</returns>
    public async Task<int> UnfollowAsync(Author followingAuthor, Author followedAuthor)
    {
        var rowsDeleted = await dbContext.Followers
            .Where(follower =>
                follower.FollowingAuthorId == followingAuthor.Id &&
                follower.FollowedAuthorId == followedAuthor.Id)
            .ExecuteDeleteAsync();

        return rowsDeleted;
    }
}
