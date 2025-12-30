using Microsoft.AspNetCore.Identity;

namespace Chirp.Core;

#nullable disable

/// <summary>
/// Represents an author (user) in the Chirp system.
/// </summary>
/// <remarks>
/// This class extends <see cref="IdentityUser"/> to integrate with ASP.NET Core Identity
/// while adding domain-specific functionality for the Chirp application.
/// An author can create and own multiple cheeps.
/// </remarks>
public class Author : IdentityUser
{
    /// <summary>
    /// Gets the collection of cheeps authored by the user.
    /// </summary>
    /// <remarks>
    /// This navigation property represents a one-to-many relationship
    /// between an author and their cheeps.
    /// </remarks>
    public ICollection<Cheep> Cheeps { get; init; }
}
