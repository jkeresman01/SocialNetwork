using System;
using System.Collections.Generic;

namespace SocialNetwork.Domain.Models;

public partial class FriendRequest
{
    public long Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public string Status { get; set; } = null!;

    public DateTime UpdatedAt { get; set; }

    public long ReceiverId { get; set; }

    public long SenderId { get; set; }

    public virtual User Receiver { get; set; } = null!;

    public virtual User Sender { get; set; } = null!;
}
