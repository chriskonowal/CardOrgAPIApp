using CardOrgAPI.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardOrgAPI.Application.Players.Search
{
    public class SearchPlayersResponse
    {
        public IEnumerable<PlayerResponse> Players { get; set; }
        public int Total { get; set; }
    }
}
