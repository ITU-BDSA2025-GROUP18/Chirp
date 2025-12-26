using Chirp.Core;
using Chirp.Core.DTOs;

namespace Chirp.Repositories;

public interface IAuthorRepository
{
    public Task<AuthorDTO?> GetPersonalDataAsync(string authorName);
    public Task<Author?> GetAuthorFromNameAsync(string name);
    public Task<Author?> GetAuthorFromEmailAsync(string email);
}
