using CardOrgAPI.Entities;
using CardOrgAPI.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardOrgAPI.Converters
{
    public static class GradeCompanyConverter
    {
        public static GradeCompanyResponse Convert(GradeCompany source)
        {
            if (source == null)
            {
                return null;
            }

            return new GradeCompanyResponse()
            {
                GradeCompanyId = source.GradeCompanyId,
                Name = source.Name
            };
        }
    }
}
