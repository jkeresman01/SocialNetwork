using System;
using System.Collections.Generic;

namespace SocialNetwork.Domain.Models;

public partial class User
{
    public long Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public string Email { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? ProfileImageId { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<Comment> Comment { get; set; } = [];

    public virtual ICollection<FriendRequest> FriendRequestReceiver { get; set; } = [];

    public virtual ICollection<FriendRequest> FriendRequestSender { get; set; } = [];

    public virtual ICollection<Post> Post { get; set; } = [];

    public virtual ICollection<Rating> Rating { get; set; } = [];

    public virtual ICollection<User> Friend { get; set; } = [];

    public virtual ICollection<User> UserNavigation { get; set; } = [];
}
