using CardOrgAPI.Entities;
using CardOrgAPI.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardOrgAPI.Converters
{
    public static class TeamCardConverter
    {
        public static TeamCardResponse Convert(TeamCard source)
        {
            if (source == null)
            {
                return null;
            }

            return new TeamCardResponse()
            {
                CardId = source.CardId,
                TeamCardId = source.TeamCardId,
                TeamId = source.TeamId,
                Team = TeamConverter.Convert(source.Team)
            };
        }
    }
}
