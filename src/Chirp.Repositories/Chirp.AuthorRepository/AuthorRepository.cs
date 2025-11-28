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
}
