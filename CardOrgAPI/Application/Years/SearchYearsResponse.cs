using CardOrgAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardOrgAPI.Application.Years
{
    public class SearchYearsResponse
    {
        public IEnumerable<Year> Years { get; set; }
        public int TotalYears { get; set; }
    }
}
