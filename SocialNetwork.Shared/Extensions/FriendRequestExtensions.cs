using SocialNetwork.Domain.Enums;
using SocialNetwork.Domain.Models;

namespace SocialNetwork.Shared.Extensions;

public static class FriendRequestExtensions
{
    public static FriendRequestStatus GetStatusEnum(this FriendRequest request)
        => Enum.TryParse<FriendRequestStatus>(request.Status, out var result) ? result : FriendRequestStatus.Pending;

    public static void SetStatusEnum(this FriendRequest request, FriendRequestStatus status) 
        => request.Status = status.ToString();
}

