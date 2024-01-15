using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.FetchResponses
{
    public class RawgFetchResponse<T> where T : BaseEntity
    {
        public int Count { get; set; }
        public string? Next { get; set; }
        public IEnumerable<T> Results { get; set; }
    }
}
