using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Application.DTOs;
using SocialNetwork.Application.Interfaces;
using SocialNetwork.Application.Payloads;
using SocialNetwork.Infrastructure.Data;
using SocialNetwork.Shared.Models;
using SocialNetwork.Domain.Exceptions;

namespace SocialNetwork.Application.Services
{
    public class UserService(AppDbContext context, IMapper mapper) : IUserService
    {
        private readonly AppDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<PagedResult<UserSummaryDTO>> GetAllUsersAsync(int page, int size)
        {
            var query = _context.Users.AsNoTracking();

            var totalCount = await query.CountAsync();
            var users = await query
                .OrderBy(u => u.Id)
                .Skip(page * size)
                .Take(size)
                .ToListAsync();

            return new PagedResult<UserSummaryDTO>
            {
                Items = _mapper.Map<List<UserSummaryDTO>>(users),
                Page = page,
                Size = size,
                TotalCount = totalCount
            };
        }

        public async Task<UserDTO> GetUserByIdAsync(long id)
        {
            var user = await _context.Users.FindAsync(id);

            return user == null ? throw new ResourceNotFoundException($"User with ID {id} not found.") : _mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO> UpdateUserByEmailAsync(string email, UserUpdateRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email) ?? throw new ResourceNotFoundException($"User with email '{email}' not found.");
            _mapper.Map(request, user);

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return _mapper.Map<UserDTO>(user);
        }

        public async Task DeleteUserByIdAsync(long id)
        {
            var user = await _context.Users.FindAsync(id) ?? throw new ResourceNotFoundException($"User with ID {id} not found.");
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<byte[]> GetProfileImageAsync(long userId)
        {
            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == userId);

                return [];
        }
    }
}

