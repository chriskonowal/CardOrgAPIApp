using System;
using System.Collections.Generic;

#nullable disable

namespace CardOrgAPI.Entities
{
    public partial class Team
    {
        public Team()
        {
            SearchSortTeams = new HashSet<SearchSortTeam>();
            TeamCards = new HashSet<TeamCard>();
        }

        public int TeamId { get; set; }
        public string City { get; set; }
        public string Name { get; set; }

        public virtual ICollection<SearchSortTeam> SearchSortTeams { get; set; }
        public virtual ICollection<TeamCard> TeamCards { get; set; }
    }
}
