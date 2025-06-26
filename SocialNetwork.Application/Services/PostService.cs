using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Application.DTOs;
using SocialNetwork.Application.Interfaces;
using SocialNetwork.Application.Payloads;
using SocialNetwork.Domain.Exceptions;
using SocialNetwork.Domain.Models;
using SocialNetwork.Infrastructure.Data;
using SocialNetwork.Shared.Models;

namespace SocialNetwork.Application.Services;

public class PostService(AppDbContext context, IMapper mapper) : IPostService
{
    private readonly AppDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<PagedResult<PostDTO>> GetAllPostsAsync(int page, int size)
    {
        var query = _context.Posts
            .Include(p => p.User)
            .OrderByDescending(p => p.CreatedAt)
            .AsNoTracking();

        var totalCount = await query.CountAsync();

        var posts = await query
            .Skip(page * size)
            .Take(size)
            .ToListAsync();

        return new PagedResult<PostDTO>
        (
            _mapper.Map<List<PostDTO>>(posts),
            page,
            size,
            totalCount
        );
    }

    public async Task<PostDTO> GetPostByIdAsync(long id)
    {
        var post = await _context.Posts
            .Include(p => p.User)
            .FirstOrDefaultAsync(p => p.Id == id)
            ?? throw new ResourceNotFoundException($"Post with ID {id} not found.");

        return _mapper.Map<PostDTO>(post);
    }

    public async Task<PostDTO> CreatePostAsync(string userEmail, CreatePostRequest request)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == userEmail)
            ?? throw new ResourceNotFoundException($"User with email '{userEmail}' not found.");

        var post = _mapper.Map<Post>(request);
        post.UserId = user.Id;
        post.CreatedAt = DateTime.UtcNow;

        _context.Posts.Add(post);
        await _context.SaveChangesAsync();

        return _mapper.Map<PostDTO>(post);
    }

    public async Task<List<PostDTO>> GetPostsByUserIdAsync(long userId)
    {
        var posts = await _context.Posts
            .Include(p => p.User)
            .Where(p => p.UserId == userId)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();

        return _mapper.Map<List<PostDTO>>(posts);
    }

    public async Task RatePostAsync(long postId, string userEmail, int stars)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail)
            ?? throw new ResourceNotFoundException($"User with email '{userEmail}' not found.");

        var rating = await _context.Ratings.FirstOrDefaultAsync(r => r.PostId == postId && r.UserId == user.Id);

        if (rating == null)
        {
            rating = new Rating
            {
                UserId = user.Id,
                PostId = postId,
                Stars = stars,
                CreatedAt = DateTime.UtcNow
            };
            _context.Ratings.Add(rating);
        }
        else
        {
            rating.Stars = stars;
            rating.UpdatedAt = DateTime.UtcNow;
            _context.Ratings.Update(rating);
        }

        await _context.SaveChangesAsync();
    }

    public async Task CommentOnPostAsync(long postId, string userEmail, string content)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail)
            ?? throw new ResourceNotFoundException($"User with email '{userEmail}' not found.");

        var comment = new Comment
        {
            UserId = user.Id,
            PostId = postId,
            Content = content,
            CreatedAt = DateTime.UtcNow
        };

        _context.Comments.Add(comment);
        await _context.SaveChangesAsync();
    }

    public async Task<List<CommentDTO>> GetCommentsFromPostAsync(long postId)
    {
        var comments = await _context.Comments
            .Where(c => c.PostId == postId)
            .Include(c => c.User)
            .OrderBy(c => c.CreatedAt)
            .ToListAsync();

        return _mapper.Map<List<CommentDTO>>(comments);
    }

    public async Task<PostDTO> UpdatePostByIdAsync(long postId, string userEmail, UpdatePostRequest request)
    {
        var post = await _context.Posts
            .Include(p => p.User)
            .FirstOrDefaultAsync(p => p.Id == postId)
            ?? throw new ResourceNotFoundException($"Post with ID {postId} not found.");

        if (!string.Equals(post.User.Email, userEmail, StringComparison.OrdinalIgnoreCase))
        {
            throw new ForbiddenAccessException("You are not authorized to update this post.");
        }

        post.Title = request.Title;
        post.Content = request.Content;
        post.UpdatedAt = DateTime.UtcNow;

        _context.Posts.Update(post);
        await _context.SaveChangesAsync();

        return _mapper.Map<PostDTO>(post);
    }

    public async Task DeletePostByIdAsync(long postId, string userEmail)
    {
        var post = await _context.Posts
            .Include(p => p.User)
            .FirstOrDefaultAsync(p => p.Id == postId)
            ?? throw new ResourceNotFoundException($"Post with ID {postId} not found.");

        if (!string.Equals(post.User.Email, userEmail, StringComparison.OrdinalIgnoreCase))
            throw new ForbiddenAccessException("You are not authorized to delete this post.");

        _context.Posts.Remove(post);
        await _context.SaveChangesAsync();
    }

}

