using CardOrgAPI.Entities;
using CardOrgAPI.QueryFilters;
using CardOrgAPI.Requests;
using System.Linq;

namespace CardOrgAPI.Converters
{
    public static class CardQueryFilterConverter
    {
        public static CardQueryFilter Convert(CardRequest source)
        {
            if (source == null)
            {
                return null;
            }

            return new CardQueryFilter()
            {
                CardDescription = source.CardDescription,
                CardId = source.CardId,
                CardNumber = source.CardNumber,
                Copies = source.Copies,
                EbayPrice = source.EbayPrice,
                Grade = source.Grade,
                HighestBeckettPrice = source.HighestBeckettPrice,
                IsAutograph = source.IsAutograph,
                IsGameWornJersey = source.IsGameWornJersey,
                IsGraded = source.IsGraded,
                IsOnCardAutograph = source.IsOnCardAutograph,
                IsPatch = source.IsPatch,
                IsRookie = source.IsRookie,
                LowestBeckettPrice = source.LowestBeckettPrice,
                LowestComcprice = source.LowestComcprice,
                PricePaid = source.PricePaid,
                SerialNumber = source.SerialNumber,
                BackCardMainImagePath = source.BackCardMainImagePath,
                BackCardThumbnailImagePath = source.BackCardThumbnailImagePath,
                FrontCardMainImagePath = source.FrontCardMainImagePath,
                FrontCardThumbnailImagePath = source.FrontCardThumbnailImagePath,
                GradeCompanyId = source.GradeCompanyId,
                LocationId = source.LocationId,
                SetId = source.SetId,
                SportId = source.SportId,
                YearId = source.YearId,
                Teams = source.Teams.Select(x => x.TeamId),
                Players = source.Players.Select(x => x.PlayerId),
            };
        }

        public static Card Convert(CardQueryFilter source)
        {
            if (source == null)
            {
                return null;
            }

            return new Card()
            {
                CardDescription = source.CardDescription,
                CardId = source.CardId,
                CardNumber = source.CardNumber,
                Copies = source.Copies,
                EbayPrice = source.EbayPrice,
                Grade = source.Grade,
                HighestBeckettPrice = source.HighestBeckettPrice,
                IsAutograph = source.IsAutograph,
                IsGameWornJersey = source.IsGameWornJersey,
                IsGraded = source.IsGraded,
                IsOnCardAutograph = source.IsOnCardAutograph,
                IsPatch = source.IsPatch,
                IsRookie = source.IsRookie,
                LowestBeckettPrice = source.LowestBeckettPrice,
                LowestComcprice = source.LowestComcprice,
                PricePaid = source.PricePaid,
                SerialNumber = source.SerialNumber,
                BackCardMainImagePath = source.BackCardMainImagePath,
                BackCardThumbnailImagePath = source.BackCardThumbnailImagePath,
                FrontCardMainImagePath = source.FrontCardMainImagePath,
                FrontCardThumbnailImagePath = source.FrontCardThumbnailImagePath,
                GradeCompanyId = source.GradeCompanyId,
                LocationId = source.LocationId,
                SetId = source.SetId,
                SportId = source.SportId,
                YearId = source.YearId
            };
        }
    }
}
