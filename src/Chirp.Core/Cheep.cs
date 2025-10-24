
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Chirp.Core;

#nullable disable

// modelBuilder.Entity<Message>().Property(m => m.Text).HasMaxLength(500);

public class Cheep
{
    public int CheepId { get; set; }

    [Required]
    [StringLength(160)]
    public string Text { get; set; }
    public DateTime TimeStamp { get; set; }
    public Author Author { get; set; }
    public int AuthorId { get; set; }
}
