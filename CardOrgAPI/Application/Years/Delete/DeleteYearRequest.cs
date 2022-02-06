using CardOrgAPI.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardOrgAPI.Application.Years.Delete
{
    public class DeleteYearRequest : IRequest<ApiResponse>
    {
        public int YearId { get; set; }
    }
}
