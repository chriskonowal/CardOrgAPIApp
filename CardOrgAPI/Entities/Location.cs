using System;
using System.Collections.Generic;

#nullable disable

namespace CardOrgAPI.Entities
{
    public partial class Location
    {
        public Location()
        {
            Cards = new HashSet<Card>();
            SearchSortLocations = new HashSet<SearchSortLocation>();
        }

        public int LocationId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Card> Cards { get; set; }
        public virtual ICollection<SearchSortLocation> SearchSortLocations { get; set; }
    }
}
