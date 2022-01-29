using System;
using System.Collections.Generic;

#nullable disable

namespace CardOrgAPI.Models.Model
{
    public partial class Player
    {
        public Player()
        {
            PlayerCards = new HashSet<PlayerCard>();
        }

        public int PlayerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<PlayerCard> PlayerCards { get; set; }
    }
}
