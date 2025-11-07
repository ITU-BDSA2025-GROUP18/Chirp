using System.Globalization;

namespace Chirp.Core.Helpers;

public abstract class DateFormatter
{
    public static string TimeStampToLocalTimeString(DateTime timestamp)
    {
        return timestamp.ToLocalTime().ToString(CultureInfo.InvariantCulture);
    }
}
