using Chirp.Core;
using Chirp.Core.DTOs;
using Chirp.Core.Helpers;
using Chirp.Database;
using Microsoft.EntityFrameworkCore;

namespace Chirp.Repositories;

/// <summary>
/// Provides cheep-related data access operations using Entity Framework Core.
/// </summary>
/// <remarks>
/// This class implements <see cref="ICheepRepository"/> and interacts with the
/// <see cref="ChirpDBContext"/> to retrieve, create, and manage cheeps in the database.
/// It supports pagination, filtering by authors, posting new cheeps, and deleting
/// author-related data via the "forget me" functionality.
/// </remarks>
public class CheepRepository(ChirpDBContext dbContext) : ICheepRepository
{
    // ============== Get Endpoints ============== //

    /// <summary>
    /// Retrieves a paginated list of all cheeps ordered by timestamp descending.
    /// </summary>
    /// <param name="page">The page number (1-based) for pagination.</param>
    /// <returns>A list of <see cref="CheepDTO"/> for the specified page.</returns>
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

    /// <summary>
    /// Retrieves the total number of cheeps in the database.
    /// </summary>
    /// <returns>The total count of cheeps.</returns>
    public async Task<int> GetCheepsCountAsync()
    {
        return await dbContext.Cheeps.CountAsync();
    }

    /// <summary>
    /// Retrieves the total number of cheeps posted by a list of authors.
    /// </summary>
    /// <param name="authors">A collection of author usernames.</param>
    /// <returns>The total count of cheeps from the specified authors.</returns>
    public async Task<int> GetCheepsFromAuthorsCountAsync(IEnumerable<string> authors)
    {
        var query = dbContext.Cheeps
            .Where(cheep => authors.Contains(cheep.Author.UserName))
            .OrderByDescending(cheep => cheep.TimeStamp)
            .CountAsync();

        return await query;
    }

    /// <summary>
    /// Retrieves a paginated list of cheeps from specified authors ordered by timestamp descending.
    /// </summary>
    /// <param name="authors">A collection of author usernames.</param>
    /// <param name="page">The page number (1-based) for pagination.</param>
    /// <returns>A list of <see cref="CheepDTO"/> from the specified authors.</returns>
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

    /// <summary>
    /// Retrieves all cheeps from specified authors without pagination.
    /// </summary>
    /// <param name="authors">A collection of author usernames.</param>
    /// <returns>A list of <see cref="CheepDTO"/> from the specified authors.</returns>
    public async Task<List<CheepDTO>> GetAllCheepsFromAuthorsAsync(IEnumerable<string> authors)
    {
        var query = dbContext.Cheeps
            .Where(cheep => authors.Contains(cheep.Author.UserName))
            .OrderByDescending(cheep => cheep.TimeStamp)
            .Select(cheep => new CheepDTO
            {
                AuthorName = cheep.Author.UserName!,
                Text = cheep.Text,
                Timestamp = DateFormatter.TimeStampToLocalTimeString(cheep.TimeStamp)
            });

        return await query.ToListAsync();
    }

    // ============== Post Endpoints ============== //

    /// <summary>
    /// Creates a new cheep for a given author.
    /// </summary>
    /// <param name="author">The author posting the cheep.</param>
    /// <param name="cheepId">The unique identifier for the new cheep.</param>
    /// <param name="text">The textual content of the cheep.</param>
    /// <returns>An integer representing the number of state entries written to the database.</returns>
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

    /// <summary>
    /// Deletes all cheeps and account-related data for a given author ("forget me" functionality).
    /// </summary>
    /// <param name="author">The author requesting deletion.</param>
    /// <returns>
    /// An integer representing the number of cheeps deleted.
    /// Note: Deletion of users and followers occurs in addition to cheeps.
    /// </returns>
    public async Task<int> ForgetMeAsync(Author author)
    {
        var cheepsDeleted = await dbContext.Cheeps
            .Where(cheep => cheep.Author.UserName == author.UserName)
            .ExecuteDeleteAsync();

        var usersDeleted = await dbContext.Users
            .Where(user => user.Email == author.Email)
            .ExecuteDeleteAsync();

        var followersDeleted = await dbContext.Followers
            .Where(follower =>
                follower.FollowedAuthorId == author.Id ||
                follower.FollowingAuthorId == author.Id)
            .ExecuteDeleteAsync();

        return cheepsDeleted;
    }
}
