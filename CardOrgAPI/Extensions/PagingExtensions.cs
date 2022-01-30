using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardOrgAPI.Extensions
{
    public static class PagingExtensions
    {
        public static int PageSkip(this int page)
        {
            return (page - 1) * 10;
        }
    }
}
