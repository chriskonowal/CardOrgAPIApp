using System;
using System.Collections.Generic;

#nullable disable

namespace CardOrgAPI.Entities
{
    public partial class SearchSortTeam
    {
        public int SearchSortTeamId { get; set; }
        public int SearchSortId { get; set; }
        public int TeamId { get; set; }

        public virtual SearchSort SearchSort { get; set; }
        public virtual Team Team { get; set; }
    }
}
