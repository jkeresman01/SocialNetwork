using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Application.DTOs
{
    public class UserSummaryDTO
    {
        public long Id { get; set; }
        public string Email { get; set; } = default!;
        public string FullName { get; set; } = default!;
    }

}
