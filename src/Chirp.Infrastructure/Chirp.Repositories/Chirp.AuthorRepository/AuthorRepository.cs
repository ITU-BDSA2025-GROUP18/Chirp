using Chirp.Core;
using Chirp.Core.DTOs;
using Chirp.Database;
using Microsoft.EntityFrameworkCore;

namespace Chirp.Repositories;

/// <summary>
/// Provides author-related data access operations using Entity Framework Core.
/// </summary>
/// <remarks>
/// This class implements <see cref="IAuthorRepository"/> and interacts with
/// the <see cref="ChirpDBContext"/> to retrieve author information from the database.
/// It supports querying both domain models (<see cref="Author"/>) and DTOs (<see cref="AuthorDTO"/>).
/// </remarks>
public class AuthorRepository(ChirpDBContext dbContext) : IAuthorRepository
{
    // ============== Get Endpoints ============== //

    /// <summary>
    /// Retrieves the personal data of an author by username.
    /// </summary>
    /// <param name="authorName">The username of the author.</param>
    /// <returns>
    /// A <see cref="AuthorDTO"/> containing the author's personal data,
    /// or <c>null</c> if no matching author is found.
    /// </returns>
    public async Task<AuthorDTO?> GetPersonalDataAsync(string authorName)
    {
        var query = dbContext.Authors
            .Where(a => a.UserName == authorName)
            .Select(a => new AuthorDTO
            {
                UserName = a.UserName!,
                Email = a.Email!,
                PhoneNumber = a.PhoneNumber!
            });

        return await query.FirstOrDefaultAsync();
    }

    /// <summary>
    /// Retrieves an <see cref="Author"/> entity by username.
    /// </summary>
    /// <param name="authorName">The username of the author.</param>
    /// <returns>
    /// The <see cref="Author"/> entity if found; otherwise, <c>null</c>.
    /// </returns>
    public async Task<Author?> GetAuthorFromNameAsync(string authorName)
    {
        var query = dbContext.Authors
            .Where(author => author.UserName == authorName);

        return await query.FirstOrDefaultAsync();
    }

    /// <summary>
    /// Retrieves an <see cref="Author"/> entity by email address.
    /// </summary>
    /// <param name="email">The email address of the author.</param>
    /// <returns>
    /// The <see cref="Author"/> entity if found; otherwise, <c>null</c>.
    /// </returns>
    public async Task<Author?> GetAuthorFromEmailAsync(string email)
    {
        var query = dbContext.Authors
            .Where(author => author.Email == email);

        return await query.FirstOrDefaultAsync();
    }
}
