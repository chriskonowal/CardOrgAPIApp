using CardOrgAPI.Model;
using CardOrgAPI.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardOrgAPI.Application.Locations.Search
{
    public class SearchLocationsResponse
    {
        public IEnumerable<LocationResponse> Locations { get; set; }
        public int Total { get; set; }
    }
}
