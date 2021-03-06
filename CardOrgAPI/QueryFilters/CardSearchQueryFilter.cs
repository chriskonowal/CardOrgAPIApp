using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardOrgAPI.QueryFilters
{
    public class CardSearchQueryFilter
    {
        public string QuickSearchTerm { get; set; }
        public int RowsPerPage { get; set; }
        public int PageNumber { get; set; }

        public string SortByField { get; set; }

        public bool IsSortDesc { get; set; }

        public SearchSortQueryFilter SearchSortQueryFilter { get; set; }
    }
}
