using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chirp.Core;

#nullable disable

public class Followers
{
    public string FollowingAuthorId { get; set; }
    public string FollowingAuthorName { get; set; }

    public string FollowedAuthorId { get; set; }
    public string FollowedAuthorName { get; set; }
}
