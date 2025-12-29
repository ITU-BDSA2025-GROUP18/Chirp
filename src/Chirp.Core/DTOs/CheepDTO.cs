namespace Chirp.Core.DTOs;

/// <summary>
/// Data Transfer Object (DTO) representing a cheep (message) in the Chirp system.
/// </summary>
/// <remarks>
/// A cheep is a short text-based message posted by an author.
/// This DTO is used to transfer cheep data between application layers
/// without exposing internal domain models.
/// </remarks>
public class CheepDTO
{
    /// <summary>
    /// Gets or sets the username of the author who posted the cheep.
    /// </summary>
    /// <remarks>
    /// This value links the cheep to its corresponding author.
    /// </remarks>
    public required string AuthorName;

    /// <summary>
    /// Gets or sets the textual content of the cheep.
    /// </summary>
    /// <remarks>
    /// The text represents the main message of the cheep and is typically short in length.
    /// </remarks>
    public required string Text;

    /// <summary>
    /// Gets or sets the timestamp indicating when the cheep was created.
    /// </summary>
    /// <remarks>
    /// The timestamp is stored as a string to be displayed alongside the cheep.
    /// </remarks>
    public required string Timestamp;
}
