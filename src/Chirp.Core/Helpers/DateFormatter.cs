using System.Globalization;

namespace Chirp.Core.Helpers;

/// <summary>
/// Provides helper functionality for formatting date and time values.
/// </summary>
/// <remarks>
/// This abstract helper class contains utility methods related to date and time
/// formatting that are used across the Chirp application.
/// </remarks>
public abstract class DateFormatter
{
    /// <summary>
    /// Converts a timestamp to a localized string representation.
    /// </summary>
    /// <param name="timestamp">
    /// The <see cref="DateTime"/> value to convert to local time.
    /// </param>
    /// <returns>
    /// A string representation of the timestamp converted to local time,
    /// formatted using the invariant culture.
    /// </returns>
    /// <remarks>
    /// The method converts the provided timestamp to local time using
    /// <see cref="DateTime.ToLocalTime"/> before formatting it as a string.
    /// Using the invariant culture ensures consistent formatting across systems.
    /// </remarks>
    public static string TimeStampToLocalTimeString(DateTime timestamp)
    {
        return timestamp.ToLocalTime().ToString(CultureInfo.InvariantCulture);
    }
}
