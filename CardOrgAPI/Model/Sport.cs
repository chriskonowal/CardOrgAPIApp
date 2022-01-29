using System;
using System.Collections.Generic;

#nullable disable

namespace CardOrgAPI.Model
{
    public partial class Sport
    {
        public Sport()
        {
            Cards = new HashSet<Card>();
        }

        public int SportId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Card> Cards { get; set; }
    }
}
