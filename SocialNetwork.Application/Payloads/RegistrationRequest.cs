using System.ComponentModel.DataAnnotations;
using SocialNetwork.Domain.Enums;
using SocialNetwork.Domain.Validation;

namespace SocialNetwork.Application.Payloads;

public record RegistrationRequest(
    [Required(ErrorMessage = "Email is required")]
    string Email,

    [Required(ErrorMessage = "Password is required")]
    string Password,

    [Required(ErrorMessage = "First name is required")]
    string FirstName,

    [Required(ErrorMessage = "Last name is required")]
    string LastName,

    [Required(ErrorMessage = "Gender is required")]
    Gender Gender
);

