using CardOrgAPI.Model;
using CardOrgAPI.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardOrgAPI.Converters
{
    public static class PlayerConverter
    {
        public static PlayerResponse Convert(Player source)
        {
            if (source == null)
            {
                return null;
            }
            return new PlayerResponse()
            {
                FirstName = source.FirstName,
                LastName = source.LastName,
                PlayerId = source.PlayerId
            };
        }
    }
}
