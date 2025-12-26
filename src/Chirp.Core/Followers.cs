
namespace Chirp.Core;

#nullable disable

public class Followers
{
    public string FollowingAuthorId { get; init; }
    public string FollowingAuthorName { get; init; }

    public string FollowedAuthorId { get; init; }
    public string FollowedAuthorName { get; init; }
}
