using System;
using System.Collections.Generic;

#nullable disable

namespace CardOrgAPI.Models.Model
{
    public partial class Team
    {
        public Team()
        {
            TeamCards = new HashSet<TeamCard>();
        }

        public int TeamId { get; set; }
        public string City { get; set; }
        public string Name { get; set; }

        public virtual ICollection<TeamCard> TeamCards { get; set; }
    }
}
