using CardOrgAPI.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardOrgAPI.Application.Cards.LandingPage
{
    public class LandingPageResponse
    {
        public IEnumerable<CardResponse> Cards { get; set; }
        public int TotalCards { get; set; }
    }
}
