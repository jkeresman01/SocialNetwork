using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Application.Payloads;

public record AuthenticationRequest(
    [Required(ErrorMessage = "Email must not be blank")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    string Email,

    [Required(ErrorMessage = "Password must not be blank")]
    string Password
);

