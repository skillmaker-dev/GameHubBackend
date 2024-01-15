using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Game : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Background_Image { get; set; } = string.Empty;
        public int? Metacritic { get; set; }
        public IEnumerable<ParentPlatform>? Parent_Platforms { get; set; }
        public int? Rating_Top { get; set; }
        public string Description_Raw { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public IEnumerable<Genre>? Genres { get; set; }
        public IEnumerable<Publisher>? Publishers { get; set; }
    }
}
