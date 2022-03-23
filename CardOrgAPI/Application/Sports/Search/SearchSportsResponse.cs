using CardOrgAPI.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardOrgAPI.Application.Sports.Search
{
    public class SearchSportsResponse
    {
        public IEnumerable<SportResponse> Sports { get; set; }
        public int Total { get; set; }
    }
}
