using CardOrgAPI.Requests;
using CardOrgAPI.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardOrgAPI.Application.SearchSort.Save
{
    public class SaveSearchSortRequest : IRequest<ApiResponse>
    {
        public SearchSortRequest SearchSortRequest { get; set; }
    }
}
