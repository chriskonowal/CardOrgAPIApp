using System;
using System.Collections.Generic;

#nullable disable

namespace CardOrgAPI.Models.Model
{
    public partial class Set
    {
        public Set()
        {
            Cards = new HashSet<Card>();
        }

        public int SetId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Card> Cards { get; set; }
    }
}
