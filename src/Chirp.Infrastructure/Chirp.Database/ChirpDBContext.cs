using Chirp.Core;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Chirp.Database;

/// <summary>
/// Represents the database context for the Chirp application.
/// </summary>
/// <remarks>
/// This class extends <see cref="IdentityDbContext{TUser}"/> to include both
/// ASP.NET Core Identity tables for authentication and the Chirp-specific
/// domain entities such as <see cref="Cheep"/>, <see cref="Author"/>, and <see cref="Followers"/>.
/// </remarks>
public class ChirpDBContext(DbContextOptions<ChirpDBContext> options) : IdentityDbContext<Author>(options)
{
    /// <summary>
    /// Gets or sets the <see cref="DbSet{Cheep}"/> representing all cheeps in the database.
    /// </summary>
    public DbSet<Cheep> Cheeps { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="DbSet{Author}"/> representing all authors in the database.
    /// </summary>
    public DbSet<Author> Authors { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="DbSet{Followers}"/> representing all follower relationships in the database.
    /// </summary>
    public DbSet<Followers> Followers { get; set; }

    /// <summary>
    /// Configures the entity mappings and relationships for the database context.
    /// </summary>
    /// <param name="modelBuilder">The <see cref="ModelBuilder"/> used to configure entities.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure Cheep entity
        modelBuilder.Entity<Cheep>()
            .HasIndex(c => c.CheepId)
            .IsUnique();

        modelBuilder.Entity<Cheep>()
            .Property(c => c.Text)
            .HasMaxLength(160);

        modelBuilder.Entity<Cheep>()
            .HasOne(c => c.Author)
            .WithMany(a => a.Cheeps)
            .HasForeignKey("AuthorId");

        // Configure composite primary key for Followers entity
        modelBuilder.Entity<Followers>()
            .HasKey(f => new { f.FollowingAuthorId, f.FollowedAuthorId });
    }
}
