using CardOrgAPI.Model;
using CardOrgAPI.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardOrgAPI.Application.Years.Search
{
    public class SearchYearsResponse
    {
        public IEnumerable<YearResponse> Years { get; set; }
        public int TotalYears { get; set; }
    }
}
