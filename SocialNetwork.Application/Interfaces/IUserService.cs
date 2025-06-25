using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork.Application.DTOs;
using SocialNetwork.Shared.Models;
using SocialNetwork.Application.Payloads;

namespace SocialNetwork.Application.Interfaces
{
    public interface IUserService
    {
        Task<PagedResult<UserSummaryDTO>> GetAllUsersAsync(int page, int size);
        Task<UserDTO> GetUserByIdAsync(long id);
        Task<UserDTO> UpdateUserByEmailAsync(string email, UserUpdateRequest request);
        Task DeleteUserByIdAsync(long id);
        Task<byte[]> GetProfileImageAsync(long userId);

    }
}
