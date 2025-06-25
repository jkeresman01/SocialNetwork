using System.Collections.Generic;

namespace SocialNetwork.Shared.Models
{
    public record PagedResult<T>(
        IReadOnlyCollection<T> Items,
        int Page,
        int Size,
        int TotalCount
    );
}
