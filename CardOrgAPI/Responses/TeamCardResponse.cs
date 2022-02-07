using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardOrgAPI.Responses
{
    public class TeamCardResponse
    {
        public int TeamCardId { get; set; }
        public int TeamId { get; set; }
        public int CardId { get; set; }
        public TeamResponse Team { get; set; }
    }
}
