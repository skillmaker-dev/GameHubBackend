using Domain.Entities;

namespace Domain.FetchResponses
{
    public class RawgFetchResponse<T> where T : BaseEntity
    {
        public int Count { get; set; }
        public string? Next { get; set; }
        public IEnumerable<T> Results { get; set; }
    }
}
