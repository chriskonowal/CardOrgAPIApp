using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardOrgAPI.Responses
{
    public class CardResponse
    {
        public int CardId { get; set; }

        public IEnumerable<PlayerResponse> Players { get; set; }

        public IEnumerable<TeamResponse> Teams { get; set; }

        public GradeCompanyResponse GradeCompany { get; set; }

        public LocationResponse Location { get; set; }

        public SetResponse Set { get; set; }


        public SportResponse Sport { get; set; }

        public YearResponse Year { get; set; }

        public string CardDescription { get; set; }

        public string CardNumber { get; set; }

        public decimal LowestBeckettPrice { get; set; }

        public decimal HighestBeckettPrice { get; set; }

        public string FrontCardMainImagePath { get; set; }

        public string FrontCardThumbnailImagePath { get; set; }

        public string BackCardMainImagePath { get; set; }

        public string BackCardThumbnailImagePath { get; set; }

        public decimal LowestCOMCPrice { get; set; }

        public decimal EbayPrice { get; set; }

        public decimal PricePaid { get; set; }

        public bool IsGraded { get; set; }

        public int Copies { get; set; }

        public int SerialNumber { get; set; }

        public decimal Grade { get; set; }

        public bool IsRookie { get; set; }

        public bool IsAutograph { get; set; }

        public bool IsPatch { get; set; }

        public bool IsOnCardAutograph { get; set; }

        public bool IsGameWornJersey { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}
