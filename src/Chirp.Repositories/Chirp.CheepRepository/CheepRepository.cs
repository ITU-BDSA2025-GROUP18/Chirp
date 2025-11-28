using Chirp.Core;
using Chirp.Core.DTOs;
using Chirp.Core.Helpers;
using Chirp.Database;
using Microsoft.EntityFrameworkCore;

namespace Chirp.Repositories.CheepRepository;

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

    public async Task<int> GetCheepsFromAuthorsCountAsync(IEnumerable<string> authors)
    {
        var query = dbContext.Cheeps.Where(cheep => authors.Contains(cheep.Author.UserName))
            .OrderByDescending(cheep => cheep.TimeStamp).CountAsync();

        return await query;
    }

    public async Task<List<CheepDTO>> GetCheepsFromAuthorsAsync(IEnumerable<string> authors, int page)
    {
        var query = dbContext.Cheeps
            .Where(cheep => authors.Contains(cheep.Author.UserName))
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

    // ============== Post Endpoints ============== //
    public async Task<int> PostCheepAsync(Author author, int cheepId, string text)
    {
        dbContext.Cheeps.Add(new Cheep
        {
            CheepId = cheepId,
            Text = text,
            TimeStamp = DateTime.UtcNow,
            Author = author
        });

        return await dbContext.SaveChangesAsync();
    }
}
