using System.Diagnostics;
using Chirp.Database;

namespace Chirp.Services;

public abstract class CheepIdGenerator()
{
    private static ChirpDBContext? _dbContext;

    protected CheepIdGenerator(ChirpDBContext dbContext) : this()
    {
        _dbContext = dbContext;
    }

    public static int GetNextCheepsId()
    {
        Debug.Assert(_dbContext != null, nameof(_dbContext) + " != null");

        var cheepId = _dbContext.Cheeps.Any()
            ? _dbContext.Cheeps.OrderBy(cheep => cheep.CheepId).Last().CheepId + 1
            : 0;

        return cheepId;
    }
}
