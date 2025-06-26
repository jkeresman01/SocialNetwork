using SocialNetwork.Application.Payloads;

namespace SocialNetwork.Application.Interfaces;

public interface IAuthService
{
    Task<AuthenticationResponse> LoginAsync(AuthenticationRequest request);
    Task<AuthenticationResponse> RegisterAsync(RegistrationRequest request);
}

