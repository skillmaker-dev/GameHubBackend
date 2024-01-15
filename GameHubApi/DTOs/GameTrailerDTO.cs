using Domain.ValueObjects;

namespace GameHubApi.DTOs
{
    public class GameTrailerDTO : BaseEntityDTO
    {
        public string Name { get; set; }
        public string Preview { get; set; }

        public TrailerDataDTO Data { get; set; }
    }
}
