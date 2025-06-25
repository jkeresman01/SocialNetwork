using System;
using System.Collections.Generic;

namespace SocialNetwork.Domain.Models;

public partial class Rating
{
    public long Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public int Stars { get; set; }

    public DateTime UpdatedAt { get; set; }

    public long PostId { get; set; }

    public long UserId { get; set; }

    public virtual Post Post { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
