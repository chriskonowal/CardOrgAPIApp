using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardOrgAPI.Responses
{
    public class TeamResponse
    {
        public int TeamId { get; set; }
        public string City { get; set; }

        public string Name { get; set; }
        public string Team => $"{City} {Name}";
    }
}
