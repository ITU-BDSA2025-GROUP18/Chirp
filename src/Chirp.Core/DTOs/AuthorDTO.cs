namespace Chirp.Core.DTOs;

/// <summary>
/// Data Transfer Object (DTO) representing an author in the Chirp system.
/// </summary>
/// <remarks>
/// This DTO is used to transfer author-related data between layers of the
/// application without exposing domain or persistence models.
/// </remarks>
public class AuthorDTO
{
    /// <summary>
    /// Gets or sets the unique username of the author.
    /// </summary>
    /// <remarks>
    /// The username is used to identify the author publicly within the Chirp platform.
    /// </remarks>
    public required string UserName;

    /// <summary>
    /// Gets or sets the email address of the author.
    /// </summary>
    /// <remarks>
    /// The email address is used for contact and authentication-related purposes.
    /// </remarks>
    public required string Email;

    /// <summary>
    /// Gets or sets the phone number of the author.
    /// </summary>
    /// <remarks>
    /// The phone number may be used for account recovery or additional verification.
    /// </remarks>
    public required string PhoneNumber;
}
