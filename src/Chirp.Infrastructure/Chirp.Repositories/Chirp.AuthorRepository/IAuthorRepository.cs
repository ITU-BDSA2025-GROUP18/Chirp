using Chirp.Core;
using Chirp.Core.DTOs;

namespace Chirp.Repositories;

/// <summary>
/// Defines the contract for author-related data access operations.
/// </summary>
/// <remarks>
/// This interface provides methods to retrieve author information
/// from the database, either as domain models or as Data Transfer Objects (DTOs).
/// Implementations of this interface handle data retrieval logic for authors.
/// </remarks>
public interface IAuthorRepository
{
    /// <summary>
    /// Retrieves the personal data of an author by username.
    /// </summary>
    /// <param name="authorName">The username of the author.</param>
    /// <returns>
    /// A <see cref="AuthorDTO"/> containing the author's personal data,
    /// or <c>null</c> if the author is not found.
    /// </returns>
    public Task<AuthorDTO?> GetPersonalDataAsync(string authorName);

    /// <summary>
    /// Retrieves an <see cref="Author"/> entity by username.
    /// </summary>
    /// <param name="name">The username of the author.</param>
    /// <returns>
    /// The <see cref="Author"/> entity if found; otherwise, <c>null</c>.
    /// </returns>
    public Task<Author?> GetAuthorFromNameAsync(string name);

    /// <summary>
    /// Retrieves an <see cref="Author"/> entity by email address.
    /// </summary>
    /// <param name="email">The email address of the author.</param>
    /// <returns>
    /// The <see cref="Author"/> entity if found; otherwise, <c>null</c>.
    /// </returns>
    public Task<Author?> GetAuthorFromEmailAsync(string email);
}
