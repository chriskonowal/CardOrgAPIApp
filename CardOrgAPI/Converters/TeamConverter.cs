using CardOrgAPI.Model;
using CardOrgAPI.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardOrgAPI.Converters
{
    public static class TeamConverter
    {
        public static TeamResponse Convert(Team source)
        {
            if (source == null)
            {
                return null;
            }

            return new TeamResponse()
            {
                City = source.City,
                Name = source.Name,
                TeamId = source.TeamId
            };
        }
    }
}
