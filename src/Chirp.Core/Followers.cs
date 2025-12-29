namespace Chirp.Core;

#nullable disable

/// <summary>
/// Represents a follower relationship between two authors in the Chirp system.
/// </summary>
/// <remarks>
/// This class models the association where one author follows another.
/// It includes both the IDs and usernames of the follower and the followed author.
/// </remarks>
public class Followers
{
    /// <summary>
    /// Gets the unique identifier of the author who is following another author.
    /// </summary>
    public string FollowingAuthorId { get; init; }

    /// <summary>
    /// Gets the username of the author who is following another author.
    /// </summary>
    public string FollowingAuthorName { get; init; }

    /// <summary>
    /// Gets the unique identifier of the author being followed.
    /// </summary>
    public string FollowedAuthorId { get; init; }

    /// <summary>
    /// Gets the username of the author being followed.
    /// </summary>
    public string FollowedAuthorName { get; init; }
}
