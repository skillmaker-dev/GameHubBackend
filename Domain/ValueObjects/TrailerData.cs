using System.Text.Json.Serialization;

namespace Domain.ValueObjects
{
    public class TrailerData
    {
        [JsonPropertyName("480")]
        public string Low { get; set; }
        public string Max { get; set; }
    }
}
