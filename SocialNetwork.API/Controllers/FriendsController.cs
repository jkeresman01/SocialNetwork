using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Application.DTOs;
using SocialNetwork.Application.Interfaces;
using System.Security.Claims;

namespace SocialNetwork.API.Controllers;

[ApiController]
[Route("api/v1/friends")]
[Authorize]
public class FriendsController(IFriendService friendService) : ControllerBase
{
    private readonly IFriendService _friendService = friendService;

    [HttpPost("request/{userId:long}")]
    public async Task<IActionResult> SendRequest(long userId)
    {
        var email = User.FindFirstValue(ClaimTypes.Email)!;
        await _friendService.SendFriendRequestAsync(email, userId);
        return Ok();
    }

    [HttpPost("approve/{requestId:long}")]
    public async Task<IActionResult> ApproveRequest(long requestId)
    {
        var email = User.FindFirstValue(ClaimTypes.Email)!;
        await _friendService.ApproveRequestAsync(email, requestId);
        return Ok();
    }

    [HttpPost("decline/{requestId:long}")]
    public async Task<IActionResult> DeclineRequest(long requestId)
    {
        var email = User.FindFirstValue(ClaimTypes.Email)!;
        await _friendService.DeclineRequestAsync(email, requestId);
        return Ok();
    }

    [HttpDelete("remove/{userId:long}")]
    public async Task<IActionResult> RemoveFriend(long userId)
    {
        var email = User.FindFirstValue(ClaimTypes.Email)!;
        await _friendService.RemoveFriendAsync(email, userId);
        return Ok();
    }

    [HttpGet("requests")]
    public async Task<ActionResult<List<FriendRequestDTO>>> GetPendingRequests()
    {
        var email = User.FindFirstValue(ClaimTypes.Email)!;
        var requests = await _friendService.GetPendingRequestsAsync(email);
        return Ok(requests);
    }

    [HttpGet("list")]
    public async Task<ActionResult<List<UserSummaryDTO>>> GetFriends()
    {
        var email = User.FindFirstValue(ClaimTypes.Email)!;
        return Ok(await _friendService.GetFriendsAsync(email));
    }

    [HttpGet("non-friends")]
    public async Task<ActionResult<List<UserSummaryDTO>>> GetNonFriends()
    {
        var email = User.FindFirstValue(ClaimTypes.Email)!;
        return Ok(await _friendService.GetNonFriendsAsync(email));
    }

}

