using System;
using System.Collections.Generic;

namespace SocialNetwork.Domain.Models;

public partial class Comment
{
    public long Id { get; set; }

    public string? Content { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public long PostId { get; set; }

    public long UserId { get; set; }

    public virtual Post Post { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
