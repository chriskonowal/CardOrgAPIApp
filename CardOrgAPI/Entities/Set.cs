using System;
using System.Collections.Generic;

#nullable disable

namespace CardOrgAPI.Entities
{
    public partial class Set
    {
        public Set()
        {
            Cards = new HashSet<Card>();
            SearchSortSets = new HashSet<SearchSortSet>();
        }

        public int SetId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Card> Cards { get; set; }
        public virtual ICollection<SearchSortSet> SearchSortSets { get; set; }
    }
}
