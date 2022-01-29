using System;
using System.Collections.Generic;

#nullable disable

namespace CardOrgAPI.Models.Model
{
    public partial class Card
    {
        public Card()
        {
            PlayerCards = new HashSet<PlayerCard>();
            TeamCards = new HashSet<TeamCard>();
        }

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
        public int? SportId { get; set; }
        public int? YearId { get; set; }
        public int? SetId { get; set; }
        public int? GradeCompanyId { get; set; }
        public int? LocationId { get; set; }
        public DateTime TimeStamp { get; set; }

        public virtual GradeCompany GradeCompany { get; set; }
        public virtual Location Location { get; set; }
        public virtual Set Set { get; set; }
        public virtual Sport Sport { get; set; }
        public virtual Year Year { get; set; }
        public virtual ICollection<PlayerCard> PlayerCards { get; set; }
        public virtual ICollection<TeamCard> TeamCards { get; set; }
    }
}
