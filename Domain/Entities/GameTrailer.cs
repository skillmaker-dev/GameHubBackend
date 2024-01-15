using Domain.ValueObjects;

namespace Domain.Entities
{
    public class GameTrailer : BaseEntity
    {
        public string Name { get; set; }
        public string Preview { get; set; }

        public TrailerData Data { get; set; }
    }

}
