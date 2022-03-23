using CardOrgAPI.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardOrgAPI.Application.Sets.Search
{
    public class SearchSetsResponse
    {
        public IEnumerable<SetResponse> Sets { get; set; }
        public int Total { get; set; }
    }
}
