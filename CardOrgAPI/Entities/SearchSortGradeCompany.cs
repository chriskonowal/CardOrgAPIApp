using System;
using System.Collections.Generic;

#nullable disable

namespace CardOrgAPI.Entities
{
    public partial class SearchSortGradeCompany
    {
        public int SearchSortGradeCompanyId { get; set; }
        public int SearchSortId { get; set; }
        public int GradeCompanyId { get; set; }

        public virtual GradeCompany GradeCompany { get; set; }
        public virtual SearchSort SearchSort { get; set; }
    }
}
