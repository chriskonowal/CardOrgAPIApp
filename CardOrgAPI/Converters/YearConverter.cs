using CardOrgAPI.Application.Years.Search;
using CardOrgAPI.Model;
using CardOrgAPI.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardOrgAPI.Converters
{
    public static class YearConverter
    {
        public static YearResponse Convert(Year source)
        {
            if (source == null)
            {
                return null;
            }

            return new YearResponse() { 
                BeginningYear = source.BeginningYear,
                EndingYear = source.EndingYear,
                YearId = source.YearId
            };
        }
    }
}
