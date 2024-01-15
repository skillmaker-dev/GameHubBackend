using System.Text.Json.Serialization;

namespace GameHubApi.DTOs
{
    public class TrailerDataDTO
    {
        [JsonPropertyName("480")]
        public string Low { get; set; }
        public string Max { get; set; }
    }
}
