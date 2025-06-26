using Microsoft.EntityFrameworkCore;
using SocialNetwork.Application.DTOs;
using SocialNetwork.Application.Interfaces;
using SocialNetwork.Application.Payloads;
using SocialNetwork.Domain.Exceptions;
using SocialNetwork.Domain.Models;
using SocialNetwork.Infrastructure.Data;
using SocialNetwork.Infrastructure.Security;
using AutoMapper;
using Microsoft.Extensions.Configuration;

namespace SocialNetwork.Application.Services;

public class AuthenticationService(AppDbContext context, IMapper mapper, IConfiguration config) : IAuthService
{
    private readonly AppDbContext _context = context;
    private readonly IMapper _mapper = mapper;
    private readonly IConfiguration _config = config;

    public async Task<AuthenticationResponse> LoginAsync(AuthenticationRequest request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email)
                   ?? throw new ResourceNotFoundException("Invalid credentials.");

        var expectedHash = PasswordHashProvider.GetHash(request.Password, user.Salt);
        if (expectedHash != user.Password)
        {
            throw new ResourceNotFoundException("Invalid credentials.");
        }

        var secret = _config["Jwt:Key"];

        string token = JwtTokenProvider.CreateToken(
            _config["Jwt:Key"],
            int.Parse(_config["Jwt:ExpireMinutes"] ?? "60"),
            user.Email
        );

        return new AuthenticationResponse(token, _mapper.Map<UserDTO>(user));
    }

    public async Task<AuthenticationResponse> RegisterAsync(RegistrationRequest request)
    {
        if (await _context.Users.AnyAsync(u => u.Email == request.Email))
        {
            throw new ResourceAlreadyExistsException("Email is already in use.");
        }

        var salt = PasswordHashProvider.GetSalt();
        var hash = PasswordHashProvider.GetHash(request.Password, salt);

        var user = new User
        {
            Email = request.Email,
            Password = hash,
            Salt = salt,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Gender = request.Gender.ToString(),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        string token = JwtTokenProvider.CreateToken(
            _config["Jwt:Key"],
            int.Parse(_config["Jwt:ExpireMinutes"] ?? "60"),
            user.Email
        );

        return new AuthenticationResponse(token, _mapper.Map<UserDTO>(user));
    }
}

