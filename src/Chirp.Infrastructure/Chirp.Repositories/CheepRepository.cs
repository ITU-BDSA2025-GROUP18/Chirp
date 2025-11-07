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

    public async Task<List<CheepDTO>> GetCheepsFromAuthorAsync(string author, int page)
    {
        var query = _dbContext.Cheeps
            .Where(cheep => cheep.Author.UserName == author)
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

    public async Task<int> GetCheepsFromAuthorCountAsync(string author)
    {
        return await _dbContext.Cheeps.Where(cheep => cheep.Author.UserName == author).CountAsync();
    }

    public async Task<AuthorDTO?> GetAuthorFromNameAsync(string name)
    {
        var query = _dbContext.Authors
            .Where(author => author.UserName == name)
            .Select(author => new AuthorDTO
            {
                Name = author.UserName,
                Email = author.Email
            });

        return await query.FirstOrDefaultAsync();
    }

    public async Task<AuthorDTO?> GetAuthorFromEmailAsync(string email)
    {
        var query = _dbContext.Authors
            .Where(author => author.Email == email)
            .Select(author => new AuthorDTO
            {
                Name = author.UserName,
                Email = author.Email
            });

        return await query.FirstOrDefaultAsync();
    }

    // ============== Post Endpoints ============== //

    //TODO: Can this be moved to a service class
    public async Task<int> PostAuthorAsync(string name, string email)
    {
        // Creates an author returns the number of state entries written to the database.
        _dbContext.Authors.Add(new Author()
        {
            UserName = name,
            Email = email,
            Cheeps = new List<Cheep>()
        });

        return await _dbContext.SaveChangesAsync();
    }

    //TODO: Can this be moved to a service class
    public async Task<int> PostCheepAsync(string authorName, string text)
    {
        var query = _dbContext.Authors.Where(author => author.UserName == authorName);

        var author = await query.FirstOrDefaultAsync();

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
