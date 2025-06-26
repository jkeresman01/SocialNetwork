using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Application.DTOs
{
    public record UserSummaryDTO(
            long Id,
            String FirstName,
            String LastName,
            String Email
    );
}
