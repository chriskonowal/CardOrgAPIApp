using CardOrgAPI.Model;
using CardOrgAPI.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardOrgAPI.Application.GradeCompanies.Search
{
    public class SearchGradeCompaniesRequest : IRequest<ApiResponse<SearchGradeCompaniesResponse>>
    {
        public string SearchTerm { get; set; }
        public int RowsPerPage { get; set; }
        public int PageNumber { get; set; }

        public string SortByField { get; set; }

        public bool IsSortDesc { get; set; }
    }
}
