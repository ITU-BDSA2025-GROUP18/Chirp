

namespace Chirp.Core;

#nullable disable


public class Cheep
{
    public int CheepId { get; init; }
    public string Text { get; init; }
    public DateTime TimeStamp { get; init; }
    public Author Author { get; init; }
}
