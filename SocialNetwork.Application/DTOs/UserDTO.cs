using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Application.DTOs
{
    public record UserDTO(
        long Id,
        string FirstName,
        string LastName,
        string Email,
        string? ProfileImageId,
        string? Gender
    );
}
