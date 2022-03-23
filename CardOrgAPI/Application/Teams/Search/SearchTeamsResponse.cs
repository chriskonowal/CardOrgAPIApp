using CardOrgAPI.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardOrgAPI.Application.Teams.Search
{
    public class SearchTeamsResponse
    {
        public IEnumerable<TeamResponse> Teams { get; set; }
        public int Total { get; set; }
    }
}
