using System;
using System.Collections.Generic;

namespace SocialNetwork.Domain.Models;

public partial class Post
{
    public long Id { get; set; }

    public string? Content { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? ImageId { get; set; }

    public string? Title { get; set; }

    public DateTime UpdatedAt { get; set; }

    public long UserId { get; set; }

    public virtual ICollection<Comment> Comment { get; set; } = [];

    public virtual ICollection<Rating> Rating { get; set; } = [];

    public virtual User User { get; set; } = null!;
}
