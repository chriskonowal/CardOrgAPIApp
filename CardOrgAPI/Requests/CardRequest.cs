using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace CardOrgAPI.Requests
{
    public class CardRequest
    {
        public int CardId { get; set; }
        public string CardDescription { get; set; }
        public string CardNumber { get; set; }
        public decimal LowestBeckettPrice { get; set; }
        public decimal HighestBeckettPrice { get; set; }
        public string FrontCardMainImagePath { get; set; }
        public string FrontCardThumbnailImagePath { get; set; }
        public string BackCardMainImagePath { get; set; }
        public string BackCardThumbnailImagePath { get; set; }
        public decimal LowestComcprice { get; set; }
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
        public int SportId { get; set; }
        public int YearId { get; set; }
        public int SetId { get; set; }
        public int GradeCompanyId { get; set; }
        public int LocationId { get; set; }
        public IEnumerable<PlayerRequest> Players { get; set; }
        public IEnumerable<TeamRequest> Teams { get; set; }

        public IFormFile FrontImage { get; set; }

        public IFormFile BackImage { get; set; }

    }
}
