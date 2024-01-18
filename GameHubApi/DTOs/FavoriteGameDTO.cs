using Domain.Entities;

namespace GameHubApi.DTOs
{
    public class FavoriteGameDTO : BaseEntityDTO
    {
        public Guid UserId { get; set; }
        public string? Name { get; set; }
        public string? Slug { get; set; }
    }
}
