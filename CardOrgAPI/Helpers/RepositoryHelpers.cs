using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardOrgAPI.Helpers
{
    public static class RepositoryHelpers
    {
        public static IQueryable<T> Paging<T>(IQueryable<T> query, 
            int rowsPerPage, int pageNumber)
        {
            if (rowsPerPage > 0)
            {
                return query.Skip((pageNumber - 1) * rowsPerPage).Take(rowsPerPage);
            }
            return query;
        }
    }
}
