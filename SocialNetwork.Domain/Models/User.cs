using System;
using System.Collections.Generic;

namespace SocialNetwork.Domain.Models;

public partial class User
{
    public long Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public string Email { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string Salt { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? ProfileImageId { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = [];

    public virtual ICollection<FriendRequest> FriendRequestReceivers { get; set; } = [];

    public virtual ICollection<FriendRequest> FriendRequestSenders { get; set; } = [];

    public virtual ICollection<Post> Posts { get; set; } = [];

    public virtual ICollection<Rating> Ratings { get; set; } = [];

    public virtual ICollection<User> Friends { get; set; } = [];

    public virtual ICollection<User> UserNavigations { get; set; } = [];
}
