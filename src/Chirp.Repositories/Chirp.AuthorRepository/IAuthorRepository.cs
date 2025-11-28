using Chirp.Core.DTOs;

namespace Chirp.Repositories.AuthorRepository;

public interface IAuthorRepository
{
    public Task<AuthorDTO?> GetPersonalDataAsync(string authorName);
}
