using CardOrgAPI.Requests;
using CardOrgAPI.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardOrgAPI.Application.Cards.LandingPage
{
    public class LandingPageRequest : IRequest<ApiResponse<LandingPageResponse>>
    {
        public string QuickSearch { get; set; }
        public int RowsPerPage { get; set; }
        public int PageNumber { get; set; }

        public string SortByField { get; set; }

        public bool IsSortDesc { get; set; }

        public SearchSortRequest SearchSortRequest { get; set; }
    }
}
