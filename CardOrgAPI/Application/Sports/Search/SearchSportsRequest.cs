using CardOrgAPI.Application.Players.Search;
using CardOrgAPI.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardOrgAPI.Application.Sports.Search
{
    public class SearchSportsRequest : IRequest<ApiResponse<SearchSportsResponse>>
    {
        public string SearchTerm { get; set; }
        public int RowsPerPage { get; set; }
        public int PageNumber { get; set; }

        public string SortByField { get; set; }

        public bool IsSortDesc { get; set; }
    }
}
