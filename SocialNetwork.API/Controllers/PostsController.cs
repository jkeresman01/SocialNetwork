using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Application.DTOs;
using SocialNetwork.Application.Interfaces;
using SocialNetwork.Application.Payloads;
using SocialNetwork.Shared.Models;
using System.Security.Claims;

namespace SocialNetwork.API.Controllers
{
    [ApiController]
    [Route("api/v1/posts")]
    public class PostController(IPostService postService) : ControllerBase
    {
        private readonly IPostService _postService = postService;

        [HttpGet]
        public async Task<ActionResult<PagedResult<PostDTO>>> GetAllPosts([FromQuery] int page = 0, [FromQuery] int size = 10)
        {
            return Ok(await _postService.GetAllPostsAsync(page, size));
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<PostDTO>> GetPostById(long id)
        {
            return Ok(await _postService.GetPostByIdAsync(id));
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<PostDTO>> CreatePost([FromBody] CreatePostRequest request)
        {
            var email = User.FindFirstValue(ClaimTypes.Email)!;
            return Ok(await _postService.CreatePostAsync(email, request));
        }

        [HttpGet("user/{userId:long}")]
        public async Task<ActionResult<List<PostDTO>>> GetPostsByUserId(long userId)
        {
            return Ok(await _postService.GetPostsByUserIdAsync(userId));
        }

        [HttpPost("{id:long}/rate")]
        [Authorize]
        public async Task<IActionResult> RatePost(long id, [FromQuery] int stars)
        {
            var email = User.FindFirstValue(ClaimTypes.Email)!;
            await _postService.RatePostAsync(id, email, stars);
            return Ok();
        }

        [HttpPost("{id:long}/comments")]
        [Authorize]
        public async Task<IActionResult> CommentOnPost(long id, [FromBody] string content)
        {
            var email = User.FindFirstValue(ClaimTypes.Email)!;
            await _postService.CommentOnPostAsync(id, email, content);
            return Ok();
        }

        [HttpGet("{postId:long}/comments")]
        public async Task<ActionResult<List<CommentDTO>>> GetCommentsForPost(long postId)
        {
            return Ok(await _postService.GetCommentsFromPostAsync(postId));
        }
    }
}

