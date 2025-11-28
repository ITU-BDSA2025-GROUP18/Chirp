using Chirp.Core;
using Chirp.Core.DTOs;
using Chirp.Database;
using Microsoft.EntityFrameworkCore;

namespace Chirp.Repositories.AuthorRepository;

public class AuthorRepository(ChirpDBContext dbContext) : IAuthorRepository
{
    // ============== Get Endpoints ============== //
    public async Task<AuthorDTO?> GetPersonalDataAsync(string authorName)
    {
        var query = dbContext.Authors
            .Where(a => a.UserName == authorName)
            .Select(a => new AuthorDTO
            {
                UserName = a.UserName!,
                Email = a.Email!,
                PhoneNumber = a.PhoneNumber!
            });

        return await query.FirstOrDefaultAsync();
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
}
