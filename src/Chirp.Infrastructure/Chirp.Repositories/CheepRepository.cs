using Chirp.Core;
using Chirp.Core.DTOS;
using Chirp.Core.Helpers;
using Chirp.Database;
using Microsoft.EntityFrameworkCore;

namespace Chirp.Repositories;


public class CheepRepository(ChirpDBContext dbContext) : ICheepRepository //Queries
{
    // ============== Get Endpoints ============== //

    public async Task<List<CheepDTO>> GetCheepsAsync(int page)
    {
        var query = dbContext.Cheeps
            .OrderByDescending(cheep => cheep.TimeStamp)
            .Skip((page - 1) * 32)
            .Take(32)
            .Select(cheep => new CheepDTO
            {
                AuthorName = cheep.Author.UserName!,
                Text = cheep.Text,
                Timestamp = DateFormatter.TimeStampToLocalTimeString(cheep.TimeStamp)
            });

        return await query.ToListAsync();
    }

    public async Task<int> GetCheepsCountAsync()
    {
        return await dbContext.Cheeps.CountAsync();
    }

    public async Task<List<CheepDTO>> GetCheepsFromAuthorAsync(string authorName, int page)
    {
        var query = dbContext.Cheeps
            .Where(cheep => cheep.Author.UserName == authorName)
            .OrderByDescending(cheep => cheep.TimeStamp)
            .Skip((page - 1) * 32)
            .Take(32)
            .Select(cheep => new CheepDTO
            {
                AuthorName = cheep.Author.UserName!,
                Text = cheep.Text,
                Timestamp = DateFormatter.TimeStampToLocalTimeString(cheep.TimeStamp)
            });

        return await query.ToListAsync();
    }

    public async Task<int> GetCheepsFromAuthorCountAsync(string authorName)
    {
        return await dbContext.Cheeps.Where(cheep => cheep.Author.UserName == authorName).CountAsync();
    }

    public async Task<Author?> GetAuthorFromNameAsync(string authorName)
    {
        var query = dbContext.Authors
            .Where(author => author.UserName == authorName);

        return await query.FirstOrDefaultAsync();
    }

    public async Task<Author?> GetAuthorFromEmailAsync(string email)
    {
        var query = dbContext.Authors
            .Where(author => author.Email == email);

        return await query.FirstOrDefaultAsync();
    }

    // ============== Post Endpoints ============== //

    public async Task<int> PostCheepAsync(Author author, string text)
    {

        var cheepId = dbContext.Cheeps.Any()
            ? dbContext.Cheeps.OrderBy(cheep => cheep.CheepId).Last().CheepId + 1
            : 0;

        dbContext.Cheeps.Add(new Cheep()
        {
            CheepId = cheepId,
            Text = text,
            TimeStamp = DateTime.UtcNow,
            Author = author
        });

        return await dbContext.SaveChangesAsync();
    }
}
