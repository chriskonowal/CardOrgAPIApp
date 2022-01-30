using System;
using System.Collections.Generic;

#nullable disable

namespace CardOrgAPI.Model
{
    public partial class Year
    {
        public Year()
        {
            Cards = new HashSet<Card>();
        }

        public int YearId { get; set; }
        public int BeginningYear { get; set; }
        public int EndingYear { get; set; }

        public virtual ICollection<Card> Cards { get; set; }
    }
}
