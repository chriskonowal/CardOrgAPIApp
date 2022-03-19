using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardOrgAPI.Application.Utility.CreateCSVFiles
{
    public class CreateCSVFilesResponse
    {
        public int ProcessedAmount { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
    }
}
