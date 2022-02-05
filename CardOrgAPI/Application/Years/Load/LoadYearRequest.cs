using CardOrgAPI.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardOrgAPI.Application.Years.Load
{
    public class LoadYearRequest : IRequest<ApiResponse<YearResponse>>
    {
        public int YearId { get; set; }
    }
}
