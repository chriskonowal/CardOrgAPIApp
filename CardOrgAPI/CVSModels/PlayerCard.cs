using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardOrgAPI.CVSModels
{
    public class PlayerCard
    {
        public int PlayerCardId { get; set; }
        public int? PlayerId { get; set; }
        public int? CardId { get; set; }

    }
}
