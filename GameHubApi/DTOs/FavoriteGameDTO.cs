using Domain.Entities;

namespace GameHubApi.DTOs
{
    public class FavoriteGameDTO : BaseEntityDTO
    {
        public string? Name { get; set; }
        public string? Slug { get; set; }
    }
}
