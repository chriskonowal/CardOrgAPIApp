using CardOrgAPI.Entities;
using CardOrgAPI.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardOrgAPI.Converters
{
    public static class SetConverter
    {
        public static SetResponse Convert(Set source)
        {
            if (source == null)
            {
                return null;
            }

            return new SetResponse()
            {
                SetId = source.SetId,
                Name = source.Name
            };
        }
    }
}
