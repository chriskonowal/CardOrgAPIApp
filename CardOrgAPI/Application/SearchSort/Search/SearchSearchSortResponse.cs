using CardOrgAPI.Responses;
using System;
using System.Collections.Generic;

namespace CardOrgAPI.Application.SearchSort.Search
{
    public class SearchSearchSortResponse
    {
        public IEnumerable<SearchSortResponse> SearchSorts { get; set; }
        public int Total { get; set; }
    }
}
