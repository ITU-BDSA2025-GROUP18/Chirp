using Chirp.Core;
using Chirp.Core.Helpers;
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

    // ============== Get Endpoints ============== //

    public async Task<List<CheepDTO>> GetCheepsAsync(int page)
    {
        var query = _dbContext.Cheeps
            .OrderByDescending(cheep => cheep.TimeStamp)
            .Skip((page - 1) * 32)
            .Take(32)
            .Select(cheep => new CheepDTO
            {
                AuthorName = cheep.Author.UserName,
                Text = cheep.Text,
                Timestamp = DateFormatter.TimeStampToLocalTimeString(cheep.TimeStamp)
            });

        return await query.ToListAsync();
    }

    public async Task<int> GetCheepsCountAsync()
    {
        return await _dbContext.Cheeps.CountAsync();
    }

    public async Task<List<CheepDTO>> GetCheepsFromAuthorAsync(string authorName, int page)
    {
        var query = _dbContext.Cheeps
            .Where(cheep => cheep.Author.UserName == authorName)
            .OrderByDescending(cheep => cheep.TimeStamp)
            .Skip((page - 1) * 32)
            .Take(32)
            .Select(cheep => new CheepDTO
            {
                AuthorName = cheep.Author.UserName,
                Text = cheep.Text,
                Timestamp = DateFormatter.TimeStampToLocalTimeString(cheep.TimeStamp)
            });

        return await query.ToListAsync();
    }

    public async Task<int> GetCheepsFromAuthorCountAsync(string authorName)
    {
        return await _dbContext.Cheeps.Where(cheep => cheep.Author.UserName == authorName).CountAsync();
    }

    public async Task<Author?> GetAuthorFromNameAsync(string authorName)
    {
        var query = _dbContext.Authors
            .Where(author => author.UserName == authorName);

        return await query.FirstOrDefaultAsync();
    }

    public async Task<Author?> GetAuthorFromEmailAsync(string email)
    {
        var query = _dbContext.Authors
            .Where(author => author.Email == email);

        return await query.FirstOrDefaultAsync();
    }

    // ============== Post Endpoints ============== //

    public async Task<int> PostCheepAsync(Author author, string text)
    {

        var cheepId = _dbContext.Cheeps.Any()
            ? _dbContext.Cheeps.OrderBy(cheep => cheep.CheepId).Last().CheepId + 1
            : 0;

        _dbContext.Cheeps.Add(new Cheep()
        {
            CheepId = cheepId,
            Text = text,
            TimeStamp = DateTime.UtcNow,
            Author = author
        });

        return await _dbContext.SaveChangesAsync();
    }
}
