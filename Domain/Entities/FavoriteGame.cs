using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class FavoriteGame : BaseEntity
    {
        public string? Name { get; set; }  
        public string? Slug { get; set; }
        public string Background_Image { get; set; } = string.Empty;
    }
}
