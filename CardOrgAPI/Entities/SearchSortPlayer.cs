using System;
using System.Collections.Generic;

#nullable disable

namespace CardOrgAPI.Entities
{
    public partial class SearchSortPlayer
    {
        public int SearchSortPlayerId { get; set; }
        public int SearchSortId { get; set; }
        public int PlayerId { get; set; }

        public virtual Player Player { get; set; }
        public virtual SearchSort SearchSort { get; set; }
    }
}
