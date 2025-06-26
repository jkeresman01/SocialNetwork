namespace SocialNetwork.Application.DTOs
{
    public record PostDTO(
        long Id,
        string Title,
        string Content,
        string? ImageId,
        DateTime CreatedAt,
        DateTime? UpdatedAt,
        long UserId,
        string UserFullName
    );
}
