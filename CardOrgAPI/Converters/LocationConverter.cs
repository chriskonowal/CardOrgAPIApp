using CardOrgAPI.Entities;
using CardOrgAPI.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardOrgAPI.Converters
{
    public static class LocationConverter
    {
        public static LocationResponse Convert(Location source)
        {
            if (source == null)
            {
                return null;
            }

            return new LocationResponse()
            {
                LocationId = source.LocationId,
                Name = source.Name
            };
        }
    }
}
