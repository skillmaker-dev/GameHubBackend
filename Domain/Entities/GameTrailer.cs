using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class GameTrailer : BaseEntity
    {
        public string Name { get; set; }
        public string Preview { get; set; }

        public TrailerData Data { get; set; }
    }

}
