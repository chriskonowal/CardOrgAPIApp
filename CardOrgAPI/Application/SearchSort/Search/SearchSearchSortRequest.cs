using CardOrgAPI.Responses;
using MediatR;

namespace CardOrgAPI.Application.SearchSort.Search
{
    public class SearchSearchSortRequest : IRequest<ApiResponse<SearchSearchSortResponse>>
    {
        public string SearchTerm { get; set; }
        public int RowsPerPage { get; set; }
        public int PageNumber { get; set; }

        public string SortByField { get; set; }

        public bool IsSortDesc { get; set; }
    }
}
