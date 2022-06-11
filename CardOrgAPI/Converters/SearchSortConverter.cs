using CardOrgAPI.Entities;
using CardOrgAPI.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardOrgAPI.Converters
{
    public static class SearchSortConverter
    {
        public static SearchSort Convert(SearchSortRequest source)
        {
            if (source == null)
            {
                return new SearchSort();
            }

            return new SearchSort()
            {
                CardDescription = source.CardDescription,
                CardDescriptionSort = source.CardDescriptionSort,
                CopiesHigh = source.CopiesHigh,
                CopiesLow = source.CopiesLow,
                CopiesSort = source.CopiesSort,
                Description = source.Description,
                EbayPriceHigh = source.EbayPriceHigh,
                EbayPriceLow = source.EbayPriceLow,
                EbayPriceSort = source.EbayPriceSort,
                GradeCompanyIds = source.GradeCompanyIds,
                GradeCompanySort = source.GradeCompanySort,
                GradeHigh = source.GradeHigh,
                GradeLow = source.GradeLow,
                GradeSort = source.GradeSort,
                HasImage = source.HasImage,
                HasImageSort = source.HasImageSort,
                HighestBeckettPriceHigh = source.HighestBeckettPriceHigh,
                HighestBeckettPriceLow = source.HighestBeckettPriceLow,
                HighestBeckettPriceSort = source.HighestBeckettPriceSort,
                IsAutograph = source.IsAutograph,
                IsAutographSort = source.IsAutographSort,
                IsGameWornJersey = source.IsGameWornJersey,
                IsGameWornJerseySort = source.IsGameWornJerseySort,
                IsGraded = source.IsGraded,
                IsGradedSort = source.IsGradedSort,
                IsOnCardAutograph = source.IsOnCardAutograph,
                IsOnCardAutographSort = source.IsOnCardAutographSort,
                IsPatch = source.IsPatch,
                IsPatchSort = source.IsPatchSort,
                IsRookie = source.IsRookie,
                IsRookieSort = source.IsRookieSort,
                IsSerialNumbered = source.IsSerialNumbered,
                LocationIds = source.LocationIds,
                LocationSort = source.LocationSort,
                LowestBeckettPriceHigh = source.LowestBeckettPriceHigh,
                LowestBeckettPriceLow = source.LowestBeckettPriceLow,
                LowestBeckettPriceSort = source.LowestBeckettPriceSort,
                LowestComcpriceHigh = source.LowestComcpriceHigh,
                LowestComcpriceLow = source.LowestComcpriceLow,
                LowestComcpriceSort = source.LowestComcpriceSort,
                Name = source.Name,
                PlayerIds = source.PlayerIds,
                PlayerNameSort = source.PlayerNameSort,
                PricePaidHigh = source.PricePaidHigh,
                PricePaidLow = source.PricePaidLow,
                PricePaidSort = source.PricePaidSort,
                SearchSortId = source.SearchSortId,
                SerialNumberHigh = source.SerialNumberHigh,
                SerialNumberLow = source.SerialNumberLow,
                SerialNumberSort = source.SerialNumberSort,
                SetIds = source.SetIds,
                SetSort = source.SetSort,
                SportIds = source.SportIds,
                SportSort = source.SportSort,
                TeamIds = source.TeamIds,
                TeamSort = source.TeamSort,
                TimeStamp = source.TimeStamp,
                TimeStampEnd = source.TimeStampEnd,
                TimeStampSort = source.TimeStampSort,
                TimeStampStart = source.TimeStampStart,
                YearIds = source.YearIds,
                YearSort = source.YearSort
            };
        }
    }
}
