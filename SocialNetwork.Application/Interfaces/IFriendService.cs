using SocialNetwork.Application.DTOs;

namespace SocialNetwork.Application.Interfaces;

public interface IFriendService
{
    Task SendFriendRequestAsync(string senderEmail, long receiverId);
    Task ApproveRequestAsync(string receiverEmail, long requestId);
    Task DeclineRequestAsync(string receiverEmail, long requestId);
    Task RemoveFriendAsync(string requesterEmail, long otherUserId);
    Task<List<FriendRequestDTO>> GetPendingRequestsAsync(string receiverEmail);
}

