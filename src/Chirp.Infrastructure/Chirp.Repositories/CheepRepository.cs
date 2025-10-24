
using System.Globalization;
using Chirp.Core;
using Chirp.Database;
using Microsoft.EntityFrameworkCore;

namespace Chirp.Repositories;

#nullable disable

public class CheepDTO
{
    public string AuthorName;
    public string Text;
    public string Timestamp;
}

public class AuthorDTO
{
    public string Name;
    public string Email;
}

#nullable restore

public class CheepRepository : ICheepRepository //Queries
{
    private readonly ChirpDBContext _dbContext;

    public CheepRepository(ChirpDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<CheepDTO>> GetCheepsAsync(int page)
    {
        var query = _dbContext.Cheeps
            .OrderByDescending(cheep => cheep.TimeStamp)
            .Skip((page - 1) * 32)
            .Take(32)
            .Select(cheep => new CheepDTO
            {
                AuthorName = cheep.Author.Name,
                Text = cheep.Text,
                Timestamp = TimeStampToLocalTimeString(cheep.TimeStamp)
            });

        return await query.ToListAsync();
    }

    public async Task<List<CheepDTO>> GetCheepsFromAuthorAsync(string author, int page)
    {
        var query = _dbContext.Cheeps
            .Where(cheep => cheep.Author.Name == author)
            .OrderByDescending(cheep => cheep.TimeStamp)
            .Skip((page - 1) * 32)
            .Take(32)
            .Select(cheep => new CheepDTO
            {
                AuthorName = cheep.Author.Name,
                Text = cheep.Text,
                Timestamp = TimeStampToLocalTimeString(cheep.TimeStamp)
            });

        return await query.ToListAsync();
    }

    public async Task<List<AuthorDTO>> GetAuthorFromNameAsync(string name)
    {
        var query = _dbContext.Authors
            .Where(author => author.Name == name)

    }

    //TODO: This can be moved to a service class
    private static string TimeStampToLocalTimeString(DateTime timestamp)
    {
        return timestamp.ToLocalTime().ToString(CultureInfo.InvariantCulture);
    }
}
