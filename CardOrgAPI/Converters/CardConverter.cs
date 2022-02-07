using CardOrgAPI.Model;
using CardOrgAPI.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardOrgAPI.Converters
{
    public static class CardConverter
    {
        public static CardResponse Convert(Card source)
        {
            if (source == null)
            {
                return null;
            }

            return new CardResponse()
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
                LowestCOMCPrice = source.LowestComcprice,
                PricePaid = source.PricePaid,
                SerialNumber = source.SerialNumber,
                BackCardMainImagePath = source.BackCardMainImagePath,
                BackCardThumbnailImagePath = source.BackCardThumbnailImagePath,
                FrontCardMainImagePath = source.FrontCardMainImagePath,
                FrontCardThumbnailImagePath = source.FrontCardThumbnailImagePath,
                TimeStamp = source.TimeStamp,
                GradeCompany = GradeCompanyConverter.Convert(source.GradeCompany),
                Location = LocationConverter.Convert(source.Location),
                Set = SetConverter.Convert(source.Set),
                Sport = SportConverter.Convert(source.Sport),
                Year = YearConverter.Convert(source.Year),
                Teams = source.TeamCards.Select(x => TeamConverter.Convert(x.Team)),
                Players = source.PlayerCards.Select(x => PlayerConverter.Convert(x.Player))
            };
        }
    }
}
