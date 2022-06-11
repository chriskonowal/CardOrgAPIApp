using System;
using System.Collections.Generic;

#nullable disable

namespace CardOrgAPI.Entities
{
    public partial class SearchSortSet
    {
        public int SearchSortSetId { get; set; }
        public int SearchSortId { get; set; }
        public int SetId { get; set; }

        public virtual SearchSort SearchSort { get; set; }
        public virtual Set Set { get; set; }
    }
}
