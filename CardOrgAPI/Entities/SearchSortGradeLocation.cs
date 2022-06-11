using System;
using System.Collections.Generic;

#nullable disable

namespace CardOrgAPI.Entities
{
    public partial class SearchSortGradeLocation
    {
        public int SearchSortGradeLocationId { get; set; }
        public int SearchSortId { get; set; }
        public int LocationId { get; set; }

        public virtual Location Location { get; set; }
        public virtual SearchSort SearchSort { get; set; }
    }
}
