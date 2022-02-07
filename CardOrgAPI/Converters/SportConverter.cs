using CardOrgAPI.Model;
using CardOrgAPI.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardOrgAPI.Converters
{
    public static class SportConverter
    {
        public static SportResponse Convert(Sport source)
        {
            if (source == null)
            {
                return null;
            }

            return new SportResponse()
            {
                SportId = source.SportId,
                Name = source.Name
            };
        }
    }
}
