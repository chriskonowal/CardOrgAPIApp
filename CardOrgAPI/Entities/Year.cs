using System;
using System.Collections.Generic;

#nullable disable

namespace CardOrgAPI.Entities
{
    public partial class Year
    {
        public Year()
        {
            Cards = new HashSet<Card>();
            SearchSortYears = new HashSet<SearchSortYear>();
        }

        public int YearId { get; set; }
        public int BeginningYear { get; set; }
        public int EndingYear { get; set; }

        public virtual ICollection<Card> Cards { get; set; }
        public virtual ICollection<SearchSortYear> SearchSortYears { get; set; }
    }
}
