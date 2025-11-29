using Chirp.Core;
using Chirp.Core.DTOs;
using Chirp.Database;
using Microsoft.EntityFrameworkCore;

namespace Chirp.Repositories.FollowerRepository;

public class FollowerRepository(ChirpDBContext dbContext) : IFollowerRepository
{
    // ============== Get Endpoints ============== //
    public Task<HashSet<Followers>> AuthorFollowing(Author followingAuthor)
    {
        return Task.FromResult(dbContext.Followers.Where(follower => follower.FollowingAuthorId == followingAuthor.Id)
            .ToHashSet());
        ;
    }

    public async Task<int> AuthorFollowersCount(Author author)
    {
        var count = await
            dbContext.Followers
                .Where(follower => follower.FollowedAuthorId == author.Id)
                .CountAsync();

        return count;
    }

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

    public async Task<int> UnfollowAsync(Author followingAuthor, Author followedAuthor)
    {
        Console.WriteLine(followingAuthor + " " + followedAuthor);
        var rowsDeleted = await dbContext.Followers
            .Where(follower =>
                follower.FollowingAuthorId == followingAuthor.Id &&
                follower.FollowedAuthorId == followedAuthor.Id)
            .ExecuteDeleteAsync();

        return rowsDeleted;
    }
}
