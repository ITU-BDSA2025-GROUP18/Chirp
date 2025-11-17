using Chirp.Core;
using Chirp.Core.DTOS;
using Chirp.Core.Helpers;
using Chirp.Database;
using Microsoft.EntityFrameworkCore;

namespace Chirp.Repositories;

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

    public async Task<List<CheepDTO>> GetCheepsFromAuthorAsync(string authorName, int page)
    {
        var query = dbContext.Cheeps
            .Where(cheep => cheep.Author.UserName == authorName)
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

    public async Task<int> GetCheepsFromAuthorCountAsync(string authorName)
    {
        return await dbContext.Cheeps.Where(cheep => cheep.Author.UserName == authorName).CountAsync();
    }

    public async Task<Author?> GetAuthorFromNameAsync(string authorName)
    {
        var query = dbContext.Authors
            .Where(author => author.UserName == authorName);

        return await query.FirstOrDefaultAsync();
    }

    public async Task<Author?> GetAuthorFromEmailAsync(string email)
    {
        var query = dbContext.Authors
            .Where(author => author.Email == email);

        return await query.FirstOrDefaultAsync();
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
            FollowedAuthorId = followedAuthor.Id
        });

        AuthorFollowing(followingAuthor);
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

    public Task<HashSet<Followers>> AuthorFollowing(Author followingAuthor)
    {
        return Task.FromResult(dbContext.Followers.Where(follower => follower.FollowingAuthorId == followingAuthor.Id)
            .ToHashSet());
        ;
    }

    public async Task<Author?> GetAuthorFollowers(Author author)
    {
        //
        return null;
    }
}
