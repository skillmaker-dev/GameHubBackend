using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Platform : BaseEntity
    {
        public string Name { get; set; }
        public string Slug { get; set; }
    }
}
