namespace Chirp.Core.DTOs;

/// <summary>
/// Data Transfer Object (DTO) representing a follower relationship in the Chirp system.
/// </summary>
/// <remarks>
/// This DTO is used to transfer information about a user who follows another user,
/// without exposing internal domain or database models.
/// </remarks>
public class FollowerDTO
{
    /// <summary>
    /// Gets or sets the username of the follower.
    /// </summary>
    /// <remarks>
    /// This value identifies the user who is following another author within the Chirp platform.
    /// </remarks>
    public required string FollowerName;
}
