using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Application.DTOs
{
    public record FriendRequestDTO(
        long Id,
        long SenderId,
        string SenderFullName
    );
}
