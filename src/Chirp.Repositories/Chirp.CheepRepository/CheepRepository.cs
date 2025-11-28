using Chirp.Core;
using Chirp.Core.DTOs;
using Chirp.Core.Helpers;
using Chirp.Database;
using Microsoft.EntityFrameworkCore;

namespace Chirp.Repositories.CheepRepository;

public class CheepRepository(ChirpDBContext dbContext) : ICheepRepository //Queries
{
    // ============== Get Endpoints ============== //

    public async Task<List<CheepDTO>> GetCheepsAsync(int page)
    {
        var query = dbContext.Cheeps
            .OrderByDescending(cheep => cheep.TimeStamp)
            .Skip((page - 1) * 32)
            .Take(32)
            .Select(cheep => new CheepDTO
            {
                AuthorName = cheep.Author.UserName!,
                Text = cheep.Text,
                Timestamp = DateFormatter.TimeStampToLocalTimeString(cheep.TimeStamp)
            });

        return await query.ToListAsync();
    }

    public async Task<int> GetCheepsCountAsync()
    {
        return await dbContext.Cheeps.CountAsync();
    }

    public async Task<int> GetCheepsFromAuthorsCountAsync(IEnumerable<string> authors)
    {
        var query = dbContext.Cheeps.Where(cheep => authors.Contains(cheep.Author.UserName))
            .OrderByDescending(cheep => cheep.TimeStamp).CountAsync();

        return await query;
    }

    public async Task<List<CheepDTO>> GetCheepsFromAuthorsAsync(IEnumerable<string> authors, int page)
    {
        var query = dbContext.Cheeps
            .Where(cheep => authors.Contains(cheep.Author.UserName))
            .OrderByDescending(cheep => cheep.TimeStamp)
            .Skip((page - 1) * 32)
            .Take(32)
            .Select(cheep => new CheepDTO
            {
                AuthorName = cheep.Author.UserName!,
                Text = cheep.Text,
                Timestamp = DateFormatter.TimeStampToLocalTimeString(cheep.TimeStamp)
            });

        return await query.ToListAsync();
    }

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

    // ============== Post Endpoints ============== //

    public async Task<int> PostCheepAsync(Author author, int cheepId, string text)
    {
        dbContext.Cheeps.Add(new Cheep
        {
            CheepId = cheepId,
            Text = text,
            TimeStamp = DateTime.UtcNow,
            Author = author
        });

        return await dbContext.SaveChangesAsync();
    }

    //TODO: Move into seperate class "FollowerRepository"
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

    //TODO: Move into seperate class "FollowerRepository"
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
