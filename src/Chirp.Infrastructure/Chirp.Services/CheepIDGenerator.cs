using Chirp.Database;

namespace Chirp.Services;

/// <summary>
/// Provides helper functionality for generating unique cheep IDs.
/// </summary>
/// <remarks>
/// This abstract class contains a static method to calculate the next available
/// cheep ID based on existing cheeps in the <see cref="ChirpDBContext"/>.
/// It ensures that each cheep has a unique identifier.
/// </remarks>
public abstract class CheepIDGenerator()
{
    /// <summary>
    /// Calculates the next available cheep ID.
    /// </summary>
    /// <param name="dbContext">The <see cref="ChirpDBContext"/> used to access existing cheeps.</param>
    /// <returns>
    /// An integer representing the next available cheep ID.
    /// Returns 0 if there are no existing cheeps.
    /// </returns>
    /// <remarks>
    /// The method orders existing cheeps by <see cref="Core.Cheep.CheepId"/> and returns
    /// the highest ID plus one. This provides a simple auto-increment mechanism for cheep IDs.
    /// </remarks>
    public static int GetNextCheepsId(ChirpDBContext dbContext)
    {
        var cheepId = dbContext.Cheeps.Any()
            ? dbContext.Cheeps.OrderBy(cheep => cheep.CheepId).Last().CheepId + 1
            : 0;

        return cheepId;
    }
}
