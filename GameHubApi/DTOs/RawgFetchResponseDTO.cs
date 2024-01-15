namespace GameHubApi.DTOs
{
    public class RawgFetchResponseDTO<T> where T : BaseEntityDTO
    {
        public int Count { get; set; }
        public string? Next { get; set; }
        public IEnumerable<T> Results { get; set; }
    }
}
