using SocialNetwork.Application.DTOs;
using SocialNetwork.Application.Payloads;
using SocialNetwork.Shared.Models;

namespace SocialNetwork.Application.Interfaces;

public interface IPostService
{
    Task<PagedResult<PostDTO>> GetAllPostsAsync(int page, int size);
    Task<PostDTO> GetPostByIdAsync(long id);
    Task<PostDTO> CreatePostAsync(string userEmail, CreatePostRequest request);
    Task<List<PostDTO>> GetPostsByUserIdAsync(long userId);
    Task RatePostAsync(long postId, string userEmail, int stars);
    Task CommentOnPostAsync(long postId, string userEmail, string content);
    Task<List<CommentDTO>> GetCommentsFromPostAsync(long postId);
}

