
using Chirp.Core;
using Microsoft.EntityFrameworkCore;

namespace Chirp.Database;

public class ChirpDBContext : DbContext
{
    public DbSet<Cheep> Cheeps { get; set; }
    public DbSet<Author> Authors { get; set; }

    public ChirpDBContext(DbContextOptions<ChirpDBContext> options) : base(options)
    {
        // Seeding the database with example data
        DbInitializer.SeedDatabase(this);
    }
}
