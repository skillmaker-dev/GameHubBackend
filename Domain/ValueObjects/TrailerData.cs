using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public class TrailerData
    {
        [JsonPropertyName("480")]
        public string Low { get; set; }
        public string Max { get; set; }
    }
}
