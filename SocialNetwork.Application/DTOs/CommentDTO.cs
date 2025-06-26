namespace SocialNetwork.Application.DTOs;

public record CommentDTO(
    long Id,
    string Content,
    DateTime CreatedAt,
    long UserId,
    string UserFullName
);

