
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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Cheep>()
            .HasIndex(c => c.CheepId)
            .IsUnique();

        modelBuilder.Entity<Cheep>()
            .Property(c => c.Text)
            .HasMaxLength(160);

        modelBuilder.Entity<Author>()
            .HasIndex(c => c.Email)
            .IsUnique();
    }
}
