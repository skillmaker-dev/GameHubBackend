namespace GameHubApi.DTOs
{
    public class GameDTO : BaseEntityDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Background_Image { get; set; } = string.Empty;
        public int? Metacritic { get; set; }
        public IEnumerable<ParentPlatformDTO>? Parent_Platforms { get; set; }
        public int? Rating_Top { get; set; }
        public string Description_Raw { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public IEnumerable<GenreDTO>? Genres { get; set; }
        public IEnumerable<PublisherDTO>? Publishers { get; set; }
    }
}
