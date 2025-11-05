
using Microsoft.AspNetCore.Identity;

namespace Chirp.Core;

#nullable disable

public class Author : IdentityUser
{
    public ICollection<Cheep> Cheeps { get; set; }
}
