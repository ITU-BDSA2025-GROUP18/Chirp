using Chirp.Core;
using Chirp.Core.DTOs;

namespace Chirp.Repositories;

/// <summary>
/// Defines the contract for cheep-related data access operations.
/// </summary>
/// <remarks>
/// This interface provides methods to retrieve, create, and manage cheeps
/// in the database. Implementations handle pagination, filtering by authors,
/// and account-related operations such as "forget me".
/// </remarks>
public interface ICheepRepository
{
    /// <summary>
    /// Retrieves a paginated list of cheeps.
    /// </summary>
    /// <param name="page">The page number for pagination.</param>
    /// <returns>A list of <see cref="CheepDTO"/> representing the cheeps on the requested page.</returns>
    public Task<List<CheepDTO>> GetCheepsAsync(int page);

    /// <summary>
    /// Gets the total number of cheeps in the database.
    /// </summary>
    /// <returns>The total count of cheeps.</returns>
    public Task<int> GetCheepsCountAsync();

    /// <summary>
    /// Gets the total number of cheeps from a list of authors.
    /// </summary>
    /// <param name="authors">A collection of author usernames.</param>
    /// <returns>The total count of cheeps from the specified authors.</returns>
    public Task<int> GetCheepsFromAuthorsCountAsync(IEnumerable<string> authors);

    /// <summary>
    /// Retrieves a paginated list of cheeps from specified authors.
    /// </summary>
    /// <param name="authors">A collection of author usernames.</param>
    /// <param name="page">The page number for pagination.</param>
    /// <returns>A list of <see cref="CheepDTO"/> from the specified authors.</returns>
    public Task<List<CheepDTO>> GetCheepsFromAuthorsAsync(IEnumerable<string> authors, int page);

    /// <summary>
    /// Retrieves all cheeps from specified authors without pagination.
    /// </summary>
    /// <param name="authors">A collection of author usernames.</param>
    /// <returns>A list of <see cref="CheepDTO"/> from the specified authors.</returns>
    public Task<List<CheepDTO>> GetAllCheepsFromAuthorsAsync(IEnumerable<string> authors);

    /// <summary>
    /// Creates a new cheep for a given author.
    /// </summary>
    /// <param name="author">The author posting the cheep.</param>
    /// <param name="cheepId">The unique identifier for the new cheep.</param>
    /// <param name="text">The textual content of the cheep.</param>
    /// <returns>An integer indicating the result of the operation (e.g., rows affected).</returns>
    public Task<int> PostCheepAsync(Author author, int cheepId, string text);

    /// <summary>
    /// Deletes all cheeps associated with a given author and removes their account data.
    /// </summary>
    /// <param name="author">The author requesting deletion.</param>
    /// <returns>An integer indicating the result of the operation (e.g., rows affected).</returns>
    public Task<int> ForgetMeAsync(Author author);
}
