using System;
using System.Collections.Generic;

#nullable disable

namespace CardOrgAPI.Entities
{
    public partial class SearchSortSport
    {
        public int SearchSortSportId { get; set; }
        public int SearchSortId { get; set; }
        public int SportId { get; set; }

        public virtual SearchSort SearchSort { get; set; }
        public virtual Sport Sport { get; set; }
    }
}
