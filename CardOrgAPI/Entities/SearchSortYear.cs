using System;
using System.Collections.Generic;

#nullable disable

namespace CardOrgAPI.Entities
{
    public partial class SearchSortYear
    {
        public int SearchSortYearId { get; set; }
        public int SearchSortId { get; set; }
        public int YearId { get; set; }

        public virtual SearchSort SearchSort { get; set; }
        public virtual Year Year { get; set; }
    }
}
