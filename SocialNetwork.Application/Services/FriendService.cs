using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Application.DTOs;
using SocialNetwork.Application.Interfaces;
using SocialNetwork.Domain.Enums;
using SocialNetwork.Domain.Exceptions;
using SocialNetwork.Domain.Models;
using SocialNetwork.Infrastructure.Data;
using SocialNetwork.Shared.Extensions;

namespace SocialNetwork.Application.Services;

public class FriendService(AppDbContext context, IMapper mapper) : IFriendService
{
    private readonly AppDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task SendFriendRequestAsync(string senderEmail, long receiverId)
    {
        var sender = await GetUserByEmailAsync(senderEmail, $"Sender with email '{senderEmail}' not found.");
        var receiver = await GetUserByIdAsync(receiverId, $"Receiver with ID '{receiverId}' not found.");

        ValidateNotSelfRequest(sender.Id, receiver.Id);
        ValidateNotAlreadyFriends(sender, receiver);

        await ValidateNotAlreadyRequestedAsync(sender, receiver);

        var now = DateTime.UtcNow;

        var request = new FriendRequest
        {
            Sender = sender,
            Receiver = receiver,
            Status = FriendRequestStatus.Pending.ToString(),
            CreatedAt = now,
            UpdatedAt = now
        };

        _context.FriendRequests.Add(request);

        await _context.SaveChangesAsync();
    }

    public async Task ApproveRequestAsync(string receiverEmail, long requestId)
    {
        var receiver = await GetUserByEmailAsync(receiverEmail, $"Receiver with email '{receiverEmail}' not found.");
        var request = await GetFriendRequestByIdAsync(requestId);

        ValidateReceiverAuthorization(receiver, request);

        UpdateFriendRequestStatus(request, FriendRequestStatus.Accepted);

        request.Sender.Friends.Add(receiver);
        receiver.Friends.Add(request.Sender);

        _context.Users.UpdateRange(request.Sender, receiver);

        await _context.SaveChangesAsync();
    }

    public async Task DeclineRequestAsync(string receiverEmail, long requestId)
    {
        var receiver = await GetUserByEmailAsync(receiverEmail, $"Receiver with email '{receiverEmail}' not found.");
        var request = await GetFriendRequestByIdAsync(requestId);

        ValidateReceiverAuthorization(receiver, request);

        UpdateFriendRequestStatus(request, FriendRequestStatus.Rejected);

        await _context.SaveChangesAsync();
    }

    public async Task RemoveFriendAsync(string requesterEmail, long otherUserId)
    {
        var requester = await GetUserByEmailAsync(requesterEmail, $"User with email '{requesterEmail}' not found.");
        var other = await GetUserByIdAsync(otherUserId, $"User with ID '{otherUserId}' not found.");

        requester.Friends.Remove(other);
        other.Friends.Remove(requester);

        _context.Users.UpdateRange(requester, other);

        await _context.SaveChangesAsync();
    }

    public async Task<List<FriendRequestDTO>> GetPendingRequestsAsync(string receiverEmail)
    {
        var receiver = await GetUserByEmailAsync(receiverEmail, $"User with email '{receiverEmail}' not found.");

        var requests = await _context.FriendRequests
            .Include(r => r.Sender)
            .Where(r => r.Receiver.Id == receiver.Id && r.GetStatusEnum() == FriendRequestStatus.Pending)
            .ToListAsync();

        return _mapper.Map<List<FriendRequestDTO>>(requests);
    }

    private async Task<User> GetUserByEmailAsync(string email, string errorMessage)
        => await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email)
            ?? throw new ResourceNotFoundException(errorMessage);

    private async Task<User> GetUserByIdAsync(long id, string errorMessage)
        => await _context.Users
            .FirstOrDefaultAsync(u => u.Id == id)
            ?? throw new ResourceNotFoundException(errorMessage);

    private async Task<FriendRequest> GetFriendRequestByIdAsync(long id)
        => await _context.FriendRequests
            .Include(r => r.Sender)
            .Include(r => r.Receiver)
            .FirstOrDefaultAsync(r => r.Id == id)
            ?? throw new ResourceNotFoundException($"Friend request with ID '{id}' not found.");

    private void ValidateReceiverAuthorization(User receiver, FriendRequest request)
    {
        if (request.Receiver.Id != receiver.Id)
        {
            throw new UnauthorizedAccessException("You are not authorized to manage this friend request.");
        }
    }

    private void UpdateFriendRequestStatus(FriendRequest request, FriendRequestStatus status)
    {
        request.SetStatusEnum(status);
        request.UpdatedAt = DateTime.UtcNow;
        _context.FriendRequests.Update(request);
    }

    private void ValidateNotSelfRequest(long senderId, long receiverId)
    {
        if (senderId == receiverId)
        {
            throw new InvalidOperationException("Cannot send a friend request to yourself.");
        }
    }

    private void ValidateNotAlreadyFriends(User sender, User receiver)
    {
        if (sender.Friends.Contains(receiver))
        {
            throw new InvalidOperationException("You are already friends with this user.");
        }
    }

    private async Task ValidateNotAlreadyRequestedAsync(User sender, User receiver)
    {
        var alreadyExists = await _context.FriendRequests
            .AnyAsync(r =>
                r.Sender.Id == sender.Id &&
                r.Receiver.Id == receiver.Id &&
                r.GetStatusEnum() == FriendRequestStatus.Pending);

        if (alreadyExists)
        {
            throw new InvalidOperationException("A friend request has already been sent.");
        }
    }
}

