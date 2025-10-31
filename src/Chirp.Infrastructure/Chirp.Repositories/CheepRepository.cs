
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

    // ============== Get Endpoints ============== //

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

    public async Task<AuthorDTO?> GetAuthorFromNameAsync(string name)
    {
        var query = _dbContext.Authors
            .Where(author => author.Name == name)
            .Select(author => new AuthorDTO
            {
                Name = author.Name,
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
                Name = author.Name,
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
            AuthorId = _dbContext.Authors.Last().AuthorId + 1,
            Name = name,
            Email = email,
            Cheeps = new List<Cheep>()
        });

        return await _dbContext.SaveChangesAsync();
    }

    //TODO: Can this be moved to a service class
    public async Task<int> PostCheepAsync(Author author, string text)
    {
        _dbContext.Cheeps.Add(new Cheep()
        {
            CheepId = _dbContext.Cheeps.Last().CheepId + 1,
            Text = text,
            TimeStamp = DateTime.UtcNow,
            Author = author,
            AuthorId = author.AuthorId
        });

        return await _dbContext.SaveChangesAsync();
    }


    //TODO: Can this be moved to a service class
    private static string TimeStampToLocalTimeString(DateTime timestamp)
    {
        return timestamp.ToLocalTime().ToString(CultureInfo.InvariantCulture);
    }
}
