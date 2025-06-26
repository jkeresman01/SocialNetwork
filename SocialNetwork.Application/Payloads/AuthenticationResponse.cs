using SocialNetwork.Application.DTOs;

namespace SocialNetwork.Application.Payloads;

public record AuthenticationResponse(
    string Token,
    UserDTO User
);

