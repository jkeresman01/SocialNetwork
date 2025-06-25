using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork.Domain.Enums;
using SocialNetwork.Domain.Validation;

namespace SocialNetwork.Application.Payloads
{
    public record UserUpdateRequest(
        [Required(ErrorMessage = "Email is required")]
        [AlgebraEmail]
        string Email,

        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        string Password,

        [Required(ErrorMessage = "First name is required")]
        string FirstName,

        [Required(ErrorMessage = "Last name is required")]
        string LastName,

        [Required(ErrorMessage = "Gender is required")]
        Gender Gender
);
}
