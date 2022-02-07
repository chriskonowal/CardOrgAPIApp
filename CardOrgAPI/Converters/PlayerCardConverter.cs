using CardOrgAPI.Model;
using CardOrgAPI.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardOrgAPI.Converters
{
    public class PlayerCardConverter
    {
        public static PlayerCardResponse Convert(PlayerCard source)
        {
            if (source == null)
            {
                return null;
            }

            return new PlayerCardResponse()
            {
               CardId = source.CardId.GetValueOrDefault(),
               PlayerCardId = source.PlayerCardId,
               PlayerId = source.PlayerId.GetValueOrDefault(),
               Player = PlayerConverter.Convert(source.Player)
            };
        }
    }
}
