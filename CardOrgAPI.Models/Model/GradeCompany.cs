using System;
using System.Collections.Generic;

#nullable disable

namespace CardOrgAPI.Models.Model
{
    public partial class GradeCompany
    {
        public GradeCompany()
        {
            Cards = new HashSet<Card>();
        }

        public int GradeCompanyId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Card> Cards { get; set; }
    }
}
