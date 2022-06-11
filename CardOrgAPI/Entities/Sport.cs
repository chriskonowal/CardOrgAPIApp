using System;
using System.Collections.Generic;

#nullable disable

namespace CardOrgAPI.Entities
{
    public partial class Sport
    {
        public Sport()
        {
            Cards = new HashSet<Card>();
            SearchSortSports = new HashSet<SearchSortSport>();
        }

        public int SportId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Card> Cards { get; set; }
        public virtual ICollection<SearchSortSport> SearchSortSports { get; set; }
    }
}
