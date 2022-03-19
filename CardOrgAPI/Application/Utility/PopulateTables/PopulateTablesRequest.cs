using CardOrgAPI.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardOrgAPI.Application.Utility.PopulateTables
{
    public class PopulateTablesRequest : IRequest<ApiResponse<PopulateTablesResponse>>
    {
    }
}
