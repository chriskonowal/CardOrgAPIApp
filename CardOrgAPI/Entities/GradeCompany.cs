using System;
using System.Collections.Generic;

#nullable disable

namespace CardOrgAPI.Entities
{
    public partial class GradeCompany
    {
        public GradeCompany()
        {
            Cards = new HashSet<Card>();
            SearchSortGradeCompanies = new HashSet<SearchSortGradeCompany>();
        }

        public int GradeCompanyId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Card> Cards { get; set; }
        public virtual ICollection<SearchSortGradeCompany> SearchSortGradeCompanies { get; set; }
    }
}
