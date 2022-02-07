using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardOrgAPI.Responses
{
    public class PlayerCardResponse
    {
        public int PlayerCardId { get; set; }
        public int PlayerId { get; set; }
        public int CardId { get; set; }
        public PlayerResponse Player { get; set; }
    }
}
