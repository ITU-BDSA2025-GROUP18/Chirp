namespace Chirp.Core;

#nullable disable

/// <summary>
/// Represents a cheep (message) posted by an author in the Chirp system.
/// </summary>
/// <remarks>
/// This class defines the domain model for a cheep, including its unique identifier,
/// content, timestamp, and the author who created it.
/// </remarks>
public class Cheep
{
    /// <summary>
    /// Gets the unique identifier of the cheep.
    /// </summary>
    public int CheepId { get; init; }

    /// <summary>
    /// Gets the textual content of the cheep.
    /// </summary>
    /// <remarks>
    /// The text represents the main message posted by the author.
    /// </remarks>
    public string Text { get; init; }

    /// <summary>
    /// Gets the timestamp indicating when the cheep was created.
    /// </summary>
    public DateTime TimeStamp { get; init; }

    /// <summary>
    /// Gets the author who created the cheep.
    /// </summary>
    /// <remarks>
    /// This property represents the one-to-many relationship between
    /// an author and their cheeps.
    /// </remarks>
    public Author Author { get; init; }
}
