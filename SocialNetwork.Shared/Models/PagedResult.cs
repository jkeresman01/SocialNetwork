using System.Collections.Generic;

namespace SocialNetwork.Shared.Models
{
    public class PagedResult<T>
    {
        public List<T> Items { get; set; } = [];
        public int Page { get; set; }
        public int Size { get; set; }
        public int TotalCount { get; set; }
    }
}
