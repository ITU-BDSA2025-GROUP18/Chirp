using Chirp.Database;

namespace Chirp.Services;

public abstract class CheepIdGenerator()
{
    public static int GetNextCheepsId(ChirpDBContext dbContext)
    {
        var cheepId = dbContext.Cheeps.Any()
            ? dbContext.Cheeps.OrderBy(cheep => cheep.CheepId).Last().CheepId + 1
            : 0;

        return cheepId;
    }
}
