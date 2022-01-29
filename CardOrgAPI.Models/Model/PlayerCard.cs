using System;
using System.Collections.Generic;

#nullable disable

namespace CardOrgAPI.Models.Model
{
    public partial class PlayerCard
    {
        public int PlayerCardId { get; set; }
        public int? PlayerId { get; set; }
        public int? CardId { get; set; }

        public virtual Card Card { get; set; }
        public virtual Player Player { get; set; }
    }
}
