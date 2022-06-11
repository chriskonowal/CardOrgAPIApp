using System;
using System.Collections.Generic;

#nullable disable

namespace CardOrgAPI.Entities
{
    public partial class TeamCard
    {
        public int TeamCardId { get; set; }
        public int TeamId { get; set; }
        public int CardId { get; set; }

        public virtual Card Card { get; set; }
        public virtual Team Team { get; set; }
    }
}
