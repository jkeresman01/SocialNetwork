using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Application.Payloads;

public record CreatePostRequest(
    [Required(ErrorMessage = "Title is required")]
    [MaxLength(100, ErrorMessage = "Title must not exceed 100 characters")]
    string Title,

    [Required(ErrorMessage = "Content is required")]
    string Content,

    string? ImageId
);

