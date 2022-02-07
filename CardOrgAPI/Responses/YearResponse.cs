using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardOrgAPI.Responses
{
    public class YearResponse
    {
        public int YearId { get; set; }
        public int BeginningYear { get; set; }
        public int EndingYear { get; set; }

        public string Year
        {
            get
            {
                if (BeginningYear != EndingYear)
                {
                    return $"{BeginningYear}-{EndingYear}";
                }
                else
                {
                    return BeginningYear.ToString();
                }
            }
        }
    }
}
