using CardOrgAPI.Model;
using CardOrgAPI.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardOrgAPI.Application.GradeCompanies.Search
{
    public class SearchGradeCompaniesResponse
    {
        public IEnumerable<GradeCompanyResponse> GradeCompanies { get; set; }
        public int Total { get; set; }
    }
}
