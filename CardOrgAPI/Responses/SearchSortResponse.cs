using System;
using System.Collections.Generic;

namespace CardOrgAPI.Responses
{
    public class SearchSortResponse
    {
        public int SearchSortId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsGraded { get; set; }
        public bool IsRookie { get; set; }
        public bool IsAutograph { get; set; }
        public bool IsPatch { get; set; }
        public bool IsOnCardAutograph { get; set; }
        public bool IsGameWornJersey { get; set; }
        public IEnumerable<PlayerResponse> PlayerIds { get; set; }
        public IEnumerable<TeamResponse> TeamIds { get; set; }
        public IEnumerable<SportResponse> SportIds { get; set; }
        public IEnumerable<YearResponse> YearIds { get; set; }
        public IEnumerable<SetResponse> SetIds { get; set; }
        public IEnumerable<GradeCompanyResponse> GradeCompanyIds { get; set; }
        public IEnumerable<LocationResponse> LocationIds { get; set; }
        public string CardDescription { get; set; }
        public decimal LowestBeckettPriceLow { get; set; }
        public decimal LowestBeckettPriceHigh { get; set; }
        public decimal HighestBeckettPriceLow { get; set; }
        public decimal HighestBeckettPriceHigh { get; set; }
        public decimal LowestComcpriceLow { get; set; }
        public decimal LowestComcpriceHigh { get; set; }
        public decimal EbayPriceLow { get; set; }
        public decimal EbayPriceHigh { get; set; }
        public decimal PricePaidLow { get; set; }
        public decimal PricePaidHigh { get; set; }
        public decimal GradeLow { get; set; }
        public decimal GradeHigh { get; set; }
        public int CopiesLow { get; set; }
        public int CopiesHigh { get; set; }
        public int SerialNumberLow { get; set; }
        public int SerialNumberHigh { get; set; }
        public bool HasImage { get; set; }
        public DateTime? TimeStampStart { get; set; }
        public DateTime? TimeStampEnd { get; set; }
        public int PlayerNameSort { get; set; }
        public int TeamSort { get; set; }
        public int CardDescriptionSort { get; set; }
        public int LowestBeckettPriceSort { get; set; }
        public int HighestBeckettPriceSort { get; set; }
        public int LowestComcpriceSort { get; set; }
        public int EbayPriceSort { get; set; }
        public int PricePaidSort { get; set; }
        public int HasImageSort { get; set; }
        public int IsGradedSort { get; set; }
        public int CopiesSort { get; set; }
        public int SerialNumberSort { get; set; }
        public int GradeSort { get; set; }
        public int IsRookieSort { get; set; }
        public int IsAutographSort { get; set; }
        public int IsPatchSort { get; set; }
        public int IsOnCardAutographSort { get; set; }
        public int IsGameWornJerseySort { get; set; }
        public bool IsSerialNumbered { get; set; }
        public int SportSort { get; set; }
        public int YearSort { get; set; }
        public int SetSort { get; set; }
        public int GradeCompanySort { get; set; }
        public int LocationSort { get; set; }
        public int TimeStampSort { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
